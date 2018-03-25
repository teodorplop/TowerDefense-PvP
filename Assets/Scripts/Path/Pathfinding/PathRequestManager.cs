using UnityEngine;
using System;
using System.Collections.Generic;
using Pathfinding;

public class PathRequestManager : MonoBehaviour {
	private static PathRequestManager _instance;

	private List<Player> _players;
	private List<Pathfinder> _pathfinders;

	void Awake() {
		_instance = this;
		_players = new List<Player>();
		_pathfinders = new List<Pathfinder>();
	}
	void OnDestroy() {
		_instance = null;
	}

	public static void Register(Player player, Pathfinder pathfinder) {
		// just in case each player has its own pathfinding grid.
		_instance._players.Add(player);
		_instance._pathfinders.Add(pathfinder);
	}

	// Leave it with callbacks for now, we might want to send this requests to a separate thread.
	public static void RequestPath(Player player, Vector3 start, Vector3 target, Action<bool, Vector3[]> callback) {
		int idx = _instance._players.IndexOf(player);
		_instance._pathfinders[idx].FindPath(start, target, callback);
	}

	public static Vector3 GetConvenientPoint(Player player, Vector3 center, float range) {
		int idx = _instance._players.IndexOf(player);
		return _instance._pathfinders[idx].FindMinimumPenaltyPoint(center, range);
	}
}
