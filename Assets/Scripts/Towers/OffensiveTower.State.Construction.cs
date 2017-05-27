using System.Collections;
using UnityEngine;

public partial class OffensiveTower {
	protected IEnumerator Construction_EnterState() {
		yield return new WaitForSeconds(_constructionTime);

		SetState(TowerState.Active);
	}
}
