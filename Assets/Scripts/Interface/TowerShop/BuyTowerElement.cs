using UnityEngine;
using Ingame.towers;

namespace Interface.towerShop {
	public class BuyTowerElement : TowerElement {
		private string towerName;
		public string TowerName { get { return towerName; } }

		public override void Inject(TowerFactory factory, Tower tower, string upgrade) {
			base.Inject(factory, tower, upgrade);

			this.towerName = upgrade;
			_texture.sprite = StreamingAssets.GetSprite(factory.GetTowerUISprite(towerName));
			_valueLabel.value = factory.GetUpgradeCost(tower.owner, towerName);
		}

		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new UpgradeTowerRequest(Players.ClientPlayer.Id, _tower.name, towerName));
		}
	}
}
