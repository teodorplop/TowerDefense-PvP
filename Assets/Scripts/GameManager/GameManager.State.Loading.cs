using System.Collections;
using UnityEngine;
using Pathfinding;

public partial class GameManager {
	private Player _clientPlayer, _serverPlayer;
	public Player ClientPlayer { get { return _clientPlayer; } }
	public Player ServerPlayer { get { return _serverPlayer; } }
	public Player GetPlayer(string name) {
		if (_clientPlayer.Name == name) {
			return _clientPlayer;
		}
		if (_serverPlayer.Name == name) {
			return _serverPlayer;
		}
		Debug.LogError("Player with name " + name + " not found.");
		return null;
	}

	IEnumerator Loading_EnterState() {
		GameObject playerContainer = GameObject.Find("PlayerContainer");
		_clientPlayer = GeneratePlayer(playerContainer, "ClientPlayer", true);

		/*playerContainer = Instantiate(playerContainer);
		playerContainer.transform.position = new Vector3(55.0f, 0.0f, 0.0f);
		playerContainer.transform.localScale = Vector3.one;
		_serverPlayer = GeneratePlayer(playerContainer, "ServerPlayer", false);*/

		_uiManager.Inject(_clientPlayer.Wallet);

		yield return null;

		SetState(GameState.Idle);
	}

	private Player GeneratePlayer(GameObject container, string name, bool clientPlayer) {
		container.name = name;

		Grid grid = new Grid(_gridNodeRadius, container.GetComponentInChildren<TerrainInfo>());
		Pathfinder pathfinder = new Pathfinder(grid);
		TowerFactory towerFactory = container.GetComponentInChildren<TowerFactory>();
		MonsterFactory monsterFactory = container.GetComponentInChildren<MonsterFactory>();
		PathsContainer pathsContainer = container.GetComponentInChildren<PathsContainer>();

		Player player = new Player(name, clientPlayer, Wallet.Clone(_wallet), pathsContainer, pathfinder, towerFactory);
		
		Tower[] towers = towerFactory.GetComponentsInChildren<Tower>();
		foreach (Tower tower in towers) {
			player.Register(tower);
		}

		_wavesManager.Register(player, monsterFactory);

		return player;
	}
}
