using System.Collections.Generic;

public class Players {
	private static Players _instance;
	private static Players Instance {
		get {
			if (_instance == null) {
				_instance = new Players();
			}
			return _instance;
		}
	}

	private List<Player> _players;
	private Players() {
		_players = new List<Player>();
	}

	public static void OnDestroy() {
		if (_instance != null) {
			_instance._players = null;
			_instance = null;
		}
	}
	public static void Register(Player player) {
		Instance._players.Add(player);
	}
	public static void Unregister(Player player) {
		Instance._players.Remove(player);
	}

	public static Player[] GetPlayers() {
		return Instance._players.ToArray();
	}
	public static Player GetPlayer(string name) {
		return Instance._players.Find(obj => obj.Name == name);
	}

	public static Player ClientPlayer {
		get { return Instance._players.Find(obj => obj.ClientPlayer); }
	}
}
