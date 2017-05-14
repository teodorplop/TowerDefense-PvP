using UnityEngine;

namespace Interface.towershop {
	public class SellTowerElement : TowerElement {
		[SerializeField]
		private IntLabel _sellValueLabel;

		public override void Inject(Tower tower) {
			base.Inject(tower);
		}

		public override void OnPress() {
		}
	}
}
