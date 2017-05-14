using UnityEngine;

namespace Interface.towershop {
	public class ArcherTowerElement : TowerElement {
		public override void OnPress() {
			GameManager.Instance.HandleRequest(new UpgradeTowerRequest(_tower.name, "ArcherTower"));
		}
	}
}
