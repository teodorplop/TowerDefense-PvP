using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseUnit {
	private int _waypointIndex;
	private Path _waypointsPath;

	[SerializeField]
	private float _movementSpeed = 5.0f;
	[SerializeField]
	private float _turnSpeed = 5.0f;

	protected virtual void OnPathReached() {

	}

	protected virtual void Walking_FixedUpdate() {
		if (_waypointsPath == null) {
			return;
		}

		if (_waypointIndex == _waypointsPath.turnBoundaries.Length) {
			// We passed through all our waypoints, so request another path.
			OnPathReached();
			return;
		}

		Vector3 position = transform.position - owner.WorldOffset;
		Vector2 position2D = new Vector2(position.x, position.z);

		if (_waypointsPath.turnBoundaries[_waypointIndex].HasCrossedLine(position2D)) {
			++_waypointIndex;

			if (_waypointIndex == _waypointsPath.turnBoundaries.Length) {
				OnPathReached();
				return;
			}
		}

		Quaternion targetRotation = Quaternion.LookRotation(_waypointsPath.waypoints[_waypointIndex] - position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * _turnSpeed);
		transform.Translate(Vector3.forward * Time.fixedDeltaTime * _movementSpeed);
	}
}
