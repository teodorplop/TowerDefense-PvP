using UnityEngine;

public partial class Monster {
	[SerializeField]
	private float _movementSpeed;

	private int _waypointIndex;
	private Vector3[] _waypoints;

	void Walking_FixedUpdate() {
		Vector3 position = transform.position - owner.WorldOffset;
		float distanceFromWaypoint = Vector3.Distance(position, _waypoints[_waypointIndex]);

		if (distanceFromWaypoint <= 0.05f) {
			++_waypointIndex;
			
			if (_waypointIndex >= _waypoints.Length) {
				++_pathIndex;
				SetState(MonsterState.Idle);
				return;
			}
		}

		Vector3 currentWaypoint = _waypoints[_waypointIndex] + owner.WorldOffset;
		transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, _movementSpeed * Time.fixedDeltaTime);
	}

	void Walking_OnDrawGizmos() {
		if (_debug) {
			for (int i = _waypointIndex; i < _waypoints.Length; ++i) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(_waypoints[i], Vector3.one);

				if (i == _waypointIndex) {
					Gizmos.DrawLine(transform.position, _waypoints[i] + owner.WorldOffset);
				} else {
					Gizmos.DrawLine(_waypoints[i - 1] + owner.WorldOffset, _waypoints[i] + owner.WorldOffset);
				}
			}
		}
	}
}
