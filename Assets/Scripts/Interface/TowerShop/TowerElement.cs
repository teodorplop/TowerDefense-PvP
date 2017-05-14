using UnityEngine;

namespace Interface.towershop {
	public class TowerElement : MonoBehaviour {
		protected Tower _tower;

		public virtual void Inject(Tower tower) {
			_tower = tower;
		}
		public virtual void OnPress() {

		}
	}
}
