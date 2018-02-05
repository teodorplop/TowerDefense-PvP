using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit {
	protected override void Fighting_Update() {
		ResetIdleTime();

		if (Target != null && Target.Target == this) {
			base.Fighting_Update();
			return;
		}

		Monster newTarget = GetMonsterInRange();
		if (newTarget != null && newTarget.Target == null) {
			RemoveTarget();
			Engage(newTarget);
			newTarget.Engage(this);
		} else {
			base.Fighting_Update();
		}
	}

	protected override void OnFightingEnded() {
		base.OnFightingEnded();
		SetState(UnitState.ReturnToRallyPoint);
	}
}
