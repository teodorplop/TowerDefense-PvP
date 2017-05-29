using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit {
	protected override void Fighting_FixedUpdate() {
		if (Target != null && Target.Target == this) {
			base.Fighting_FixedUpdate();
			return;
		}

		Monster newTarget = GetMonsterInRange();
		if (newTarget != null && newTarget.Target == null) {
			RemoveTarget();
			Engage(newTarget);
			newTarget.Engage(this);
		} else {
			base.Fighting_FixedUpdate();
		}
	}

	protected override void OnFightingEnded() {
		base.OnFightingEnded();
		SetState(BaseUnitState.Idle);
	}
}
