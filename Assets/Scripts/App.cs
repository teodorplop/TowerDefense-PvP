using System;
using UnityEngine;

/// <summary>
/// TODO: make this a state machine
/// </summary>
public class App : MonoBehaviour {
	[Serializable]
	private class App_Data {
		[SerializeField] private string versionNumber = "1.00";
		[SerializeField] private string buildStage = "Alpha";

		public string VersionNumber { get { return versionNumber; } }
		public string BuildStage { get { return buildStage; } }
	}

	private static App instance;
	private App_Data appData;

	private UserProfile profile;
	private MatchInfo matchInfo;

	private GameServer server;
	private MatchmakingServer mmServer;
	private RTServer rtServer;

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		
		instance = this;
		DontDestroyOnLoad(gameObject);
		
		InitAppData();
		InitServer();

		EventManager.AddListener<ProfileChangedEvent>(OnProfileChanged);
	}
	void Start() {
		if (UserPrefs.RememberMe) Login(UserPrefs.Username, UserPrefs.Password);
	}
	void OnDestroy() {
		if (instance == this) {
			EventManager.RemoveListener<ProfileChangedEvent>(OnProfileChanged);
			instance = null;
		}
	}

	void InitAppData() {
		TextAsset asset = Resources.Load<TextAsset>("App");
		if (asset == null)
			appData = new App_Data();
		else {
			appData = JsonSerializer.Deserialize<App_Data>(asset.text);
			Resources.UnloadAsset(asset);
		}

		MacroSystem.SetMacroValue("VERSION_NUMBER", appData.VersionNumber);
		MacroSystem.SetMacroValue("BUILD_STAGE", appData.BuildStage);
	}

	void InitServer() {
		server = new GameServer();
	}

	public static void Exit() {
		Application.Quit();
	}

	public static void Register(string username, string email, string password) {
		EventManager.Raise(new ServerRequestEvent("STR_registerInProgress"));
		instance.server.Register(username, username, password, instance.RegisterCallback);
	}
	private void RegisterCallback(bool success) {
		EventManager.Raise(new ServerResponseEvent(success ? "STR_registerSuccess" : "STR_registerFailed"));
	}

	public static void Login(string username, string password) {
		EventManager.Raise(new ServerRequestEvent("STR_loginInProgress"));
		instance.server.Login(username, password, (profile) => { instance.LoginCallback(username, password, profile); } );
	}
	private void LoginCallback(string username, string password, UserProfile profile) {
		EventManager.Raise(new ServerResponseEvent(profile != null ? "STR_loginSuccess" : "STR_loginFailed"));

		this.profile = profile;

		if (profile != null) {
			if (UserPrefs.RememberMe) {
				UserPrefs.Username = username;
				UserPrefs.Password = password;
			}

			mmServer = new MatchmakingServer(profile);

			MacroSystem.SetMacroValue("USERNAME", username);
			MacroSystem.SetMacroValue("MMR", profile.MMR);
			SceneLoader.LoadScene("Menu");
		}
	}

	public static void Logout() {
		UserPrefs.RememberMe = false;
		SceneLoader.LoadScene("Login");
		instance.server.Logout();
		EventManager.Raise(new LoggedOutEvent());
	}

	public static void FindMatch(Mod selectedMod) {
		GameResources.SetMod(selectedMod.Name);

		instance.mmServer.FindMatch(selectedMod, delegate(bool success) {
			Debug.Log("FindMatch: " + success);
			if (success) EventManager.Raise(new FindMatchStartedEvent());
		}, instance.OnMatchFound);
	}

	public static void CancelFindMatch() {
		instance.mmServer.CancelFindMatch(delegate(bool success) {
			Debug.Log("CancelMatch: " + success);
			if (success) EventManager.Raise(new FindMatchCanceledEvent());
		});
	}

	private void OnMatchFound(MatchInfo matchInfo) {
		if (matchInfo == null) {
			Debug.Log("Match not found.");
			PlayerInfo client = new PlayerInfo(profile.DisplayName, "0", 0, profile.MMR);
			PlayerInfo ai = new PlayerInfo(AIPlayer.GenerateAIName(), "1", 1, profile.MMR);
			matchInfo = new MatchInfo(client, ai);
		}

		this.matchInfo = matchInfo;

		for (int i = 0; i < matchInfo.Players.Count; ++i) {
			MacroSystem.SetMacroValue(string.Format("PLAYER{0}_DISPLAYNAME", i), matchInfo.Players[i].DisplayName);
			MacroSystem.SetMacroValue(string.Format("PLAYER{0}_MMR", i), matchInfo.Players[i].MMR);
		}

		EventManager.Raise(new MatchFoundEvent());

		SceneLoader.LoadScene("Splash", OnSplashLoaded);
	}
	private void OnSplashLoaded() {
		rtServer = new RTServer(server, matchInfo);
		rtServer.Connect(OnRTReady);
	}
	private void OnRTReady(bool ready) {
		if (ready)
			SceneLoader.LoadSceneAdditive(matchInfo.Map, OnMapLoaded);
	}
	private void OnMapLoaded() {
		// mini hack so new GameObjects will be parented to Game scene
		SceneLoader.SetActiveScene(GameManager.Instance.gameObject.scene);
		GameManager.Instance.StartLoading(matchInfo, OnGameLoaded);
	}
	private void OnGameLoaded() {
		SceneLoader.UnloadScene("Splash", delegate {
			GameManager.Instance.StartMatch();
		});
	}

	private void OnProfileChanged(ProfileChangedEvent evt) {
		MacroSystem.SetMacroValue("MMR", profile.MMR);
	}
}
