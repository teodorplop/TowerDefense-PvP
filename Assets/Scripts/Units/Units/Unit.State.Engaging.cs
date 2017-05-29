using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit {
	protected override void OnEngageTargetLost() {
		base.OnEngageTargetLost();
		SetState(BaseUnitState.Idle);
	}
}
