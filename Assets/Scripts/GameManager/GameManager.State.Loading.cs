using System.Collections;
using UnityEngine;
using Pathfinding;

public partial class GameManager {
	IEnumerator Loading_EnterState() {
		TerrainInfo terrain = FindObjectOfType<TerrainInfo>();
		Grid grid = new Grid(_gridNodeRadius, _gridBlurSize, terrain);
		Pathfinder pathfinder = new Pathfinder(grid);

		GameObject playerContainer = GameObject.Find("PlayerContainer");
		Player clientPlayer = GeneratePlayer(playerContainer, "ClientPlayer", true);

		playerContainer = Instantiate(playerContainer);
		playerContainer.transform.localScale = Vector3.one;
		playerContainer.transform.position = new Vector3(55.0f, 0.0f, 0.0f);
		Player serverPlayer = GeneratePlayer(playerContainer, "ServerPlayer", false);

		Players.Register(clientPlayer);
		Players.Register(serverPlayer);

		// We may want to have one pathfinder for each player, in case we want dynamic pathfinding.
		PathRequestManager.Register(clientPlayer, pathfinder);
		PathRequestManager.Register(serverPlayer, pathfinder);

		_uiManager.Inject(clientPlayer.Wallet);

		yield return null;

		// we should make sure both players are connected, bla bla, and then start the match
		_wavesManager.StartMatch();
		SetState(GameState.Idle);
	}

	private Player GeneratePlayer(GameObject container, string name, bool clientPlayer) {
		container.name = name;

		Player player = new Player(name, clientPlayer, Wallet.Clone(_wallet), container.transform);
		
		Tower[] towers = container.GetComponentsInChildren<Tower>();
		foreach (Tower tower in towers) {
			player.Register(tower);
		}

		_wavesManager.Register(player);

		return player;
	}
}
