using UnityEngine;

namespace Interface.towershop {
	public class ArcherTowerElement : TowerElement {
		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new UpgradeTowerRequest(GameManager.Instance.ClientPlayer.Name, _tower.name, "ArcherTower"));
		}
	}
}
