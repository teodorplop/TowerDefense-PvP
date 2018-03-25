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
	private GameServer server;

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		
		instance = this;
		DontDestroyOnLoad(gameObject);
		InitAppData();
		InitServer();
	}
	void Start() {
		if (UserPrefs.RememberMe) Login(UserPrefs.Username, UserPrefs.Password);
	}
	void OnDestroy() {
		if (instance == this) instance = null;
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

		if (profile != null) {
			if (UserPrefs.RememberMe) {
				UserPrefs.Username = username;
				UserPrefs.Password = password;
			}
		}
	}
}
