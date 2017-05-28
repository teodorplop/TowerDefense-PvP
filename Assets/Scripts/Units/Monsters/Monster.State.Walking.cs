using UnityEngine;
using Pathfinding;

public partial class Monster {
	protected override void OnPathReached(bool now) {
		if (now) {
			++_pathIndex;
		}

		if (_pathIndex == _path.Length) {
			SetState(UnitState.Destination);
		} else {
			RequestPath();
		}
	}
}
