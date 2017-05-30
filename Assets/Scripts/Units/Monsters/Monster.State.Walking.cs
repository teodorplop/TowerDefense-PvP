using UnityEngine;
using System.Collections;

public partial class Monster {
	private IEnumerator Walking_EnterState() {
		_animator.SetBool("IsWalking", true);
		yield return null;
	}

	private IEnumerator Walking_ExitState() {
		_animator.SetBool("IsWalking", false);
		yield return null;
	}

	private void OnPathReached(bool now) {
		if (now) {
			++_pathIndex;
		}

		if (_pathIndex == _path.Length) {
			SetState(MonsterState.Destination);
		} else {
			RequestPath();
		}
	}

	void Walking_FixedUpdate() {
		if (Target != null) {
			SetState(BaseUnitState.Engaging);
		} else {
			FollowWaypoints(OnPathReached);
		}
	}
}
