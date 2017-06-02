using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster {
	protected override IEnumerator Dead_EnterState() {
		GameManager.Instance.MonsterDied(this);
		return base.Dead_EnterState();
	}
}
