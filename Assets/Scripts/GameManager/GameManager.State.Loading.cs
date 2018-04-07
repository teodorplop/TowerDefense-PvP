using System;
using System.Collections;
using UnityEngine;
using Pathfinding;
using Ingame.towers;
using Grid = Pathfinding.Grid;
using UnityEngine.SceneManagement;

public partial class GameManager {
	private Action loadingCallback;

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
		AsyncOperation ui = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
		while (!ui.isDone) yield return null;
		_uiManager = FindObjectOfType<UIManager>();

		Wallet wallet = GameResources.LoadWallet();
		SendMonstersList sendMonsters = GameResources.LoadSendMonsters();

		TerrainInfo terrain = FindObjectOfType<TerrainInfo>();
		Grid grid = new Grid(_gridNodeRadius, _gridBlurSize, terrain);
		Pathfinder pathfinder = new Pathfinder(grid);

		GameObject playerContainer = GameObject.Find("PlayerContainer");
		Player clientPlayer = GeneratePlayer(playerContainer, wallet.Clone(), "ClientPlayer", true);

		playerContainer = Instantiate(playerContainer);
		playerContainer.transform.localScale = Vector3.one;
		playerContainer.transform.position = new Vector3(160.0f, 0.0f, 0.0f);
		Player serverPlayer = GeneratePlayer(playerContainer, wallet.Clone(), "ServerPlayer", false);

		// We may want to have one pathfinder for each player, in case we want dynamic pathfinding.
		PathRequestManager.Register(clientPlayer, pathfinder);
		PathRequestManager.Register(serverPlayer, pathfinder);

		_uiManager.Inject(clientPlayer.Wallet, sendMonsters);

		yield return null;

		if (loadingCallback != null) loadingCallback();
	}

	private Player GeneratePlayer(GameObject container, Wallet wallet, string name, bool clientPlayer) {
		container.name = name;

		Player player = new Player(name, clientPlayer, wallet, container.transform);
		
		Tower[] towers = container.GetComponentsInChildren<Tower>();
		foreach (Tower tower in towers) {
			player.Register(tower);
		}

		Players.Register(player);

		return player;
	}
}
