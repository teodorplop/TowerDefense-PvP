using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit {
	IEnumerator ReturnToRallyPoint_EnterState() {
		yield return StartCoroutine(RallyPoint_EnterState());
	}
	IEnumerator ReturnToRallyPoint_ExitState() {
		yield return StartCoroutine(RallyPoint_ExitState());
	}

	void ReturnToRallyPoint_FixedUpdate() {
		Monster target = GetMonsterInRange();
		if (target != null) {
			Engage(target);
			target.Engage(this);
		} else {
			FollowWaypoints(OnRallyPointReached);
		}
	}
}
