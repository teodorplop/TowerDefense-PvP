using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingame.towers;

namespace Interface.towershop {
	public class BarracksTowerElement : TowerElement {
		public override void Inject(TowerFactory factory, Tower tower) {
			base.Inject(factory, tower);

			_valueLabel.value = factory.GetUpgradeCost(tower.owner, "BarracksTower");
		}

		public override void OnPress() {
			GameManager.Instance.HandleRequest(
				new UpgradeTowerRequest(Players.ClientPlayer.Name, _tower.name, "BarracksTower"));
		}
	}
}
