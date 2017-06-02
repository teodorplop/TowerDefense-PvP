using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface.towerShop {
	public class RallyPointElement : TowerElement {
		public override void OnPress() {
			GameManager.Instance.SetRallyPoint(_tower);
		}
	}
}