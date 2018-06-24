using System.Collections;
using UnityEngine;

namespace Ingame.towers {
	public partial class Tower {
		private float _delay = 0.0f;

		public void SetConstructionDelay(float delay) {
			_delay = delay / 1000.0f;
		}

		protected IEnumerator Construction_EnterState() {
			float wait = Mathf.Max(0.0f, _constructionTime - _delay);

			yield return new WaitForSeconds(wait);

			SetState(TowerState.Active);
		}
	}
}
