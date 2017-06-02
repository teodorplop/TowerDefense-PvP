using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit {
	IEnumerator ReturnToRallyPoint_EnterState() {
		return RallyPoint_EnterState();
	}
	IEnumerator ReturnToRallyPoint_ExitState() {
		return RallyPoint_ExitState();
	}

	void ReturnToRallyPoint_FixedUpdate() {
		Monster target = GetMonsterInRange();
		if (target != null) {
			Engage(target);
			target.Engage(this);
		} else if (_refreshRallyPoint) {
			_refreshRallyPoint = false;
			RequestPath(_rallyPoint);
		} else {
			FollowWaypoints(OnRallyPointReached);
		}
	}
	
	private void ReturnToRallyPoint_OnDrawGizmos() {
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
