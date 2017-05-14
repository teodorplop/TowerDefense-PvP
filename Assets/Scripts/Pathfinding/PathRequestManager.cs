using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;

public class PathRequestManager : MonoBehaviour {
	private static PathRequestManager _instance;

	private Dictionary<Player, Pathfinder> _pathfinders;
	private Queue<PathResult> _results;

	void Awake() {
		_instance = this;
		_pathfinders = new Dictionary<Player, Pathfinder>();
		_results = new Queue<PathResult>();
	}
	void OnDestroy() {
		_instance = null;
	}

	void Update() {
		if (_results.Count > 0) {
			int count = _results.Count;
			lock (_results) {
				for (int i = 0; i < count; ++i) {
					PathResult result = _results.Dequeue();
					if (result.Callback != null) {
						result.Callback(result.Success, result.Path);
					}
				}
			}
		}
	}

	public static void Register(Player player, Pathfinder pathfinder) {
		_instance._pathfinders.Add(player, pathfinder);
	}

	public static void RequestPath(Player player, Vector3 start, Vector3 target, Action<bool, Vector3[]> callback) {
		// TODO: Proper thread here.

		_instance._pathfinders[player].FindPath(start, target, (success, path) => FindPathCallback(success, path, callback));
	}

	private static void FindPathCallback(bool success, Vector3[] path, Action<bool, Vector3[]> callback) {
		lock (_instance._results) {
			_instance._results.Enqueue(new PathResult(success, path, callback));
		}
	}
}

/*public struct PathRequest {
	private Player _player;
	private Vector3 _pathStart;
	private Vector3 _pathEnd;
	private Action<bool, Vector3[]> _callback;

	public Player Player { get { return _player; } }
	public Vector3 PathStart { get { return _pathStart; } }
	public Vector3 PathEnd { get { return _pathEnd; } }
	public Action<bool, Vector3[]> Callback { get { return _callback; } }

	public PathRequest(Player player, Vector3 start, Vector3 end, Action<bool, Vector3[]> callback) {
		_player = player;
		_pathStart = start;
		_pathEnd = end;
		_callback = callback;
	}
}
*/
public struct PathResult {
	private Vector3[] _path;
	private bool _success;
	private Action<bool, Vector3[]> _callback;

	public Vector3[] Path { get { return _path; } }
	public bool Success { get { return _success; } }
	public Action<bool, Vector3[]> Callback { get { return _callback; } }

	public PathResult(bool success, Vector3[] path, Action<bool, Vector3[]> callback) {
		_success = success;
		_path = path;
		_callback = callback;
	}
}