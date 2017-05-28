using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.towers {
	public partial class BarracksTower {
		protected IEnumerator Construction_EnterState() {
			yield return new WaitForSeconds(_constructionTime);

			SetState(TowerState.Active);
		}
	}
}
