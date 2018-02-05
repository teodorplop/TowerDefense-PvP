using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit {
	protected override void Engaging_Update() {
		ResetIdleTime();

		Monster monster = GetMonsterInRange();
		if (monster != Target && monster != null) {
			RemoveTarget();
			Engage(monster);
			monster.Engage(this);
		} else {
			base.Engaging_Update();
		}
	}

	protected override void OnEngageTargetLost() {
		base.OnEngageTargetLost();
		SetState(BaseUnitState.Idle);
	}
}
