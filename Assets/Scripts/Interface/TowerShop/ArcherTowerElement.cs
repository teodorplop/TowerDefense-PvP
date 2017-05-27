using Ingame.towers;
using UnityEngine;

namespace Interface.towershop {
	public class ArcherTowerElement : TowerElement {
		public override void Inject(TowerFactory factory, Tower tower) {
			base.Inject(factory, tower);

			_valueLabel.value = factory.GetUpgradeCost(tower.owner, "ArcherTower");
		}

		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new UpgradeTowerRequest(Players.ClientPlayer.Name, _tower.name, "ArcherTower"));
		}
	}
}
