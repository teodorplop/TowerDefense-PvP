using Pathfinding;
using UnityEngine;

public partial class BaseUnit {
	protected int _waypointIndex;
	protected Path _waypointsPath;

	[SerializeField]
	private float _movementSpeed = 5.0f;
	[SerializeField]
	private float _turnSpeed = 5.0f;

	protected virtual void OnPathReached(bool now) {
	}

	protected virtual void Walking_FixedUpdate() {
		if (_waypointsPath == null) {
			return;
		}

		if (_waypointIndex == _waypointsPath.turnBoundaries.Length) {
			// We passed through all our waypoints
			OnPathReached(false);
			return;
		}

		Vector3 position = transform.position - owner.WorldOffset;
		Vector2 position2D = new Vector2(position.x, position.z);

		if (_waypointsPath.turnBoundaries[_waypointIndex].HasCrossedLine(position2D)) {
			++_waypointIndex;

			if (_waypointIndex == _waypointsPath.turnBoundaries.Length) {
				OnPathReached(true);
				return;
			}
		}

		Quaternion targetRotation = Quaternion.LookRotation(_waypointsPath.waypoints[_waypointIndex] - position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * _turnSpeed);
		transform.Translate(Vector3.forward * Time.fixedDeltaTime * _movementSpeed);
	}

	protected void Walking_OnDrawGizmos() {
		if (_debug) {
			for (int i = _waypointIndex; i < _waypointsPath.waypoints.Length; ++i) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(_waypointsPath.waypoints[i], Vector3.one);

				if (i == _waypointIndex) {
					Gizmos.DrawLine(transform.position, _waypointsPath.waypoints[i] + owner.WorldOffset);
				} else {
					Gizmos.DrawLine(_waypointsPath.waypoints[i - 1] + owner.WorldOffset, _waypointsPath.waypoints[i] + owner.WorldOffset);
				}
			}
		}
	}
}
