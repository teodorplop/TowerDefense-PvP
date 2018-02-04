using System.Collections;
using UnityEngine;
using Pathfinding;
using Ingame.towers;
using Grid = Pathfinding.Grid;

public partial class GameManager {
	IEnumerator Loading_EnterState() {
		Wallet wallet = GameResources.LoadWallet();

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

		_uiManager.Inject(clientPlayer.Wallet);

		yield return null;

		// we should make sure both players are connected, bla bla, and then start the match
		_towerFactory.StartMatch();
		_wavesManager.StartMatch();
		SetState(GameState.Idle);
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
