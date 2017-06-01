using UnityEngine;

public partial class Monster {
	protected override void OnFightingEnded() {
		base.OnFightingEnded();
		if (Target != null) {
			SetState(BaseUnitState.Engaging);
		} else {
			SetState(BaseUnitState.Idle);
		}
	}
}
