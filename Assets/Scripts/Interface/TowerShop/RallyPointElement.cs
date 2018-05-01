using System.Collections;
using System.Collections.Generic;
using Ingame.towers;
using UnityEngine;

namespace Interface.towerShop {
	public class RallyPointElement : TowerElement {
		public override void Inject(TowerFactory factory, Tower tower, string upgrade) {
			base.Inject(factory, tower, upgrade);

			_texture.sprite = StreamingAssets.GetSprite(upgrade + "UI_Tex");
		}

		public override void OnPress() {
			GameManager.Instance.SetRallyPoint(_tower);
		}
	}
}