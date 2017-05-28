using UnityEngine;
using Pathfinding;

public partial class Monster {
	private void OnPathReached(bool now) {
		if (now) {
			++_pathIndex;
		}

		if (_pathIndex == _path.Length) {
			SetState(BaseUnitState.Destination);
		} else {
			RequestPath();
		}
	}

	void Walking_FixedUpdate() {
		FollowWaypoints(OnPathReached);
	}
}
