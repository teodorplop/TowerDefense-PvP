using UnityEngine;
using Ingame.towers;

namespace Interface.towerShop {
	public class SellTowerElement : TowerElement {
		public override void Inject(TowerFactory factory, Tower tower) {
			base.Inject(factory, tower);

			_valueLabel.value = factory.GetSellCost(tower.owner, tower.name);
		}

		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new SellTowerRequest(Players.ClientPlayer.Name, _tower.name));
		}
	}
}
