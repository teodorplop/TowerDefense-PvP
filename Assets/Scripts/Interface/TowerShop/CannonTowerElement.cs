using Ingame.towers;
using UnityEngine;

namespace Interface.towerShop {
	public class CannonTowerElement : TowerElement {
		public override void Inject(TowerFactory factory, Tower tower) {
			base.Inject(factory, tower);

			_valueLabel.value = factory.GetUpgradeCost(tower.owner, "CannonTower");
		}

		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new UpgradeTowerRequest(Players.ClientPlayer.Name, _tower.name, "CannonTower"));
		}
	}
}
