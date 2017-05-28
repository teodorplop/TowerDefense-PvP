using UnityEngine;
using Pathfinding;
using System.Collections;

public partial class Monster {
	private bool _pathRequested = false;

	IEnumerator Idle_EnterState() {
		RequestPath();
		yield return null;
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
		SetState(BaseUnitState.Walking);
	}
}
