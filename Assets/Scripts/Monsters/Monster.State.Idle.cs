using UnityEngine;
using Pathfinding;

public partial class Monster {
	private bool _pathRequested = false;

	void Idle_FixedUpdate() {
		RequestPath();
	}

	private void RequestPath() {
		if (!_pathRequested && _pathIndex < _path.Length) {
			_pathRequested = true;
			Vector3 position = transform.position - owner.WorldOffset;
			PathRequestManager.RequestPath(owner, position, _path[_pathIndex], FindPathCallback);
		}
	}

	private void FindPathCallback(bool success, Vector3[] waypoints) {
		_pathRequested = false;

		if (!success) {
			Debug.LogError(name + " couldn't find path!", gameObject);
			return;
		}

		_waypointIndex = 0;
		_waypointsPath = new Path(waypoints, transform.position - owner.WorldOffset, 5.0f);
		SetState(MonsterState.Walking);
	}
}
