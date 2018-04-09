using UnityEngine;
using Ingame.towers;

namespace Interface.towerShop {
	public class BuyTowerElement : TowerElement {
		[SerializeField]
		private string towerName;

		public override void Inject(TowerFactory factory, Tower tower) {
			base.Inject(factory, tower);
			
			_valueLabel.value = factory.GetUpgradeCost(tower.owner, towerName);
		}

		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new UpgradeTowerRequest(Players.ClientPlayer.Id, _tower.name, towerName));
		}
	}
}
