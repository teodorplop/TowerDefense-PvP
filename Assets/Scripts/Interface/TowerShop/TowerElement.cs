using UnityEngine;
using UnityEngine.UI;
using Ingame.towers;

namespace Interface.towerShop {
	public class TowerElement : MonoBehaviour {
		[SerializeField]
		protected Image _texture;
		[SerializeField]
		protected IntLabel _valueLabel;
		protected TowerFactory _factory;
		protected Tower _tower;

		public Tower tower { get { return _tower; } }

		public virtual void Inject(TowerFactory factory, Tower tower, string upgrade) {
			_tower = tower;
		}
		public virtual void OnPress() {

		}
	}
}
