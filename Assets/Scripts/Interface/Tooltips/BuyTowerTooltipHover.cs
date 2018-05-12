using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerTooltipHover : UITooltipHover {
	public override void OnEnter() {
		text = "STR_buyTowerTooltip_" + name;
		base.OnEnter();
	}
}
