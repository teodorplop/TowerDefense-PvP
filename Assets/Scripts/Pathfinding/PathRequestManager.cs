using UnityEngine;
using System;
using System.Collections.Generic;
using Pathfinding;

public class PathRequestManager : MonoBehaviour {
	private static PathRequestManager _instance;

	private Dictionary<Player, Pathfinder> _pathfinders;

	void Awake() {
		_instance = this;
		_pathfinders = new Dictionary<Player, Pathfinder>();
	}
	void OnDestroy() {
		_instance = null;
	}

	public static void Register(Player player, Pathfinder pathfinder) {
		// just in case each player has its own pathfinding grid.
		_instance._pathfinders.Add(player, pathfinder);
	}

	// Leave it with callbacks for now, we might want to send this requests to a separate thread.
	public static void RequestPath(Player player, Vector3 start, Vector3 target, Action<bool, Vector3[]> callback) {
		_instance._pathfinders[player].FindPath(start, target, callback);
	}
}
