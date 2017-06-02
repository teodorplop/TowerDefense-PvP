using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit {
	protected override void Engaging_FixedUpdate() {
		Monster monster = GetMonsterInRange();
		if (monster != Target) {
			RemoveTarget();
			Engage(monster);
			monster.Engage(this);
		} else {
			base.Engaging_FixedUpdate();
		}
	}

	protected override void OnEngageTargetLost() {
		base.OnEngageTargetLost();
		SetState(BaseUnitState.Idle);
	}
}
