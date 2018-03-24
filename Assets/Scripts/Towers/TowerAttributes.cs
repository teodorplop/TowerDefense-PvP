using System;
using UnityEngine;

namespace Ingame.towers {
	[Serializable]
	public class TowerAttributes : IGameResource {
		public string Name { get; set; }

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

		[SerializeField]
		public string trainedUnit;
		[SerializeField]
		public float respawnTimer;
		[SerializeField]
		public int maxUnits;
	}
}
