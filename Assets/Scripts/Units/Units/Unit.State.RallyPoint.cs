using System.Collections;
using UnityEngine;
using Pathfinding;

public partial class Unit {
	private bool _refreshRallyPoint;
	private Vector3 _rallyPoint;

	IEnumerator RallyPoint_EnterState() {
		_refreshRallyPoint = true;
		_animator.SetBool("IsWalking", true);
		yield return null;
	}
	IEnumerator RallyPoint_ExitState() {
		_animator.SetBool("IsWalking", false);
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

	void RallyPoint_Update() {
		if (_refreshRallyPoint) {
			_refreshRallyPoint = false;
			RequestPath(_rallyPoint);
		} else {
			FollowWaypoints(OnRallyPointReached);
		}
	}

	private void RallyPoint_OnDrawGizmos() {
		if (_debug) {
			if (_waypointsPath != null) {
				Gizmos.color = Color.black;
				for (int i = _waypointIndex; i < _waypointsPath.waypoints.Length; ++i) {
					Gizmos.DrawCube(_waypointsPath.waypoints[i] + owner.WorldOffset, Vector3.one);
				}
			}
		}
	}
}
