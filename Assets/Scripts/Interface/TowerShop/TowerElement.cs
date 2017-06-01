using UnityEngine;
using Ingame.towers;

namespace Interface.towerShop {
	public class TowerElement : MonoBehaviour {
		[SerializeField]
		protected IntLabel _valueLabel;
		protected TowerFactory _factory;
		protected Tower _tower;

		public virtual void Inject(TowerFactory factory, Tower tower) {
			_tower = tower;
		}
		public virtual void OnPress() {

		}
	}
}
