using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface.sendMonsters {
	[RequireComponent(typeof(SendMonsterElement))]
	public class SendMonsterTooltip : UITooltipHover {
		private SendMonsterElement elm;

		void Awake() {
			elm = GetComponent<SendMonsterElement>();
		}

		public override void OnEnter() {
			UITooltip.SetText("STR_sendMonsterTooltip_" + elm.Monster.name);
		}
	}
}
