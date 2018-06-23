using System;
using System.Collections;
using UnityEngine;
using Pathfinding;
using Ingame.towers;
using Grid = Pathfinding.Grid;
using UnityEngine.SceneManagement;

public partial class GameManager {
	private MatchInfo matchInfo = new MatchInfo();
	private Action loadingCallback;

	public void StartLoading(MatchInfo matchInfo, Action callback) {
		this.matchInfo = matchInfo;
		loadingCallback = callback;
		SetState(GameState.Loading);
	}
	public void StartLoading(Action callback) {
		loadingCallback = callback;
		SetState(GameState.Loading);
	}
	public void StartMatch() {
		_towerFactory.StartMatch();
		_wavesManager.StartMatch();
		SetState(GameState.Idle);
	}

	IEnumerator Loading_EnterState() {
		bool texturesLoaded = false;
		StreamingAssets.LoadTextures(delegate { texturesLoaded = true; });

		while (!texturesLoaded) yield return null;

		AsyncOperation ui = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
		while (!ui.isDone) yield return null;
		_uiManager = FindObjectOfType<UIManager>();

		Wallet wallet = GameResources.LoadWallet();
		SendMonstersList sendMonsters = GameResources.LoadSendMonsters();

		TerrainInfo terrain = FindObjectOfType<TerrainInfo>();
		Grid grid = new Grid(_gridNodeRadius, _gridBlurSize, terrain);
		Pathfinder pathfinder = new Pathfinder(grid);
		
		GameObject playerContainer = GameObject.Find("PlayerContainer");

		// Generate client player
		PlayerInfo clientPlayerInfo = matchInfo.GetClientPlayer();
		Player clientPlayer = GeneratePlayer(playerContainer, wallet.Clone(), clientPlayerInfo.Id, clientPlayerInfo.DisplayName, true);
		PathRequestManager.Register(clientPlayer, pathfinder);
		_uiManager.Inject(clientPlayer.Wallet, _monsterFactory, sendMonsters);
		
		// Generate other players
		Player serverPlayer = null;
		int idx = 1;
		foreach (PlayerInfo playerInfo in matchInfo.Players) {
			if (playerInfo.clientPlayer) continue;
			
			playerContainer = Instantiate(playerContainer);
			playerContainer.transform.localScale = Vector3.one;
			playerContainer.transform.position = new Vector3(idx * 160.0f, 0.0f, 0.0f);
			serverPlayer = GeneratePlayer(playerContainer, wallet.Clone(), playerInfo.Id, playerInfo.DisplayName, false);
			PathRequestManager.Register(serverPlayer, pathfinder);

			if (matchInfo.IsFake)
				AIPlayer.GenerateAI(serverPlayer);

			++idx;
		}

		_uiManager.Inject(Players.GetPlayers());

		yield return null;

		if (loadingCallback != null) loadingCallback();
	}

	private Player GeneratePlayer(GameObject container, Wallet wallet, string id, string name, bool clientPlayer) {
		container.name = name;

		Player player = new Player(id, name, clientPlayer, wallet, container.transform);
		
		Tower[] towers = container.GetComponentsInChildren<Tower>();
		foreach (Tower tower in towers) {
			player.Register(tower);
		}

		Players.Register(player);

		return player;
	}
}
