using System.Collections;
using UnityEngine;
using Pathfinding;

public partial class Unit {
	private Vector3 _rallyPoint;

	IEnumerator RallyPoint_EnterState() {
		RequestPath(_rallyPoint);
		yield return null;
	}

	private void RequestPath(Vector3 target) {
		PathRequestManager.RequestPath(owner, transform.position - owner.WorldOffset, target - owner.WorldOffset, FindPathCallback);
	}
	private void FindPathCallback(bool success, Vector3[] waypoints) {
		if (!success) {
			Debug.LogError(name + " couldn't find path!", gameObject);
			return;
		}

		_waypointIndex = 0;
		_waypointsPath = new Path(waypoints, transform.position - owner.WorldOffset, 5.0f);
	}

	private void OnRallyPointReached(bool now) {
		SetState(BaseUnitState.Idle);
	}

	void RallyPoint_FixedUpdate() {
		FollowWaypoints(OnRallyPointReached);
	}
}
