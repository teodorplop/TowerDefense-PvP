using System;
using UnityEngine;

namespace Ingame.towers {
	[Serializable]
	public class TowerAttributes {
		[SerializeField]
		public string[] upgrades;

		[SerializeField]
		public int cost;
		[SerializeField]
		public float sellValue;

		[SerializeField]
		public float attackSpeed;
		[SerializeField]
		public float range;
		[SerializeField]
		public int attackDamage;

		public TowerAttributes Clone() {
			TowerAttributes clone = new TowerAttributes();
			clone.upgrades = new string[upgrades.Length];
			upgrades.CopyTo(clone.upgrades, 0);

			clone.cost = cost;
			clone.sellValue = sellValue;

			clone.attackSpeed = attackSpeed;
			clone.range = range;
			clone.attackDamage = attackDamage;

			return clone;
		}
	}
}
