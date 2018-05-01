using UnityEngine;
using Ingame.towers;

namespace Interface.towerShop {
	public class SellTowerElement : TowerElement {
		public override void Inject(TowerFactory factory, Tower tower, string upgrade) {
			base.Inject(factory, tower, upgrade);

			_texture.sprite = StreamingAssets.GetSprite(upgrade + "UI_Tex");
			_valueLabel.value = factory.GetSellCost(tower.owner, tower.name);
		}

		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new SellTowerRequest(Players.ClientPlayer.Id, _tower.name));
		}
	}
}
