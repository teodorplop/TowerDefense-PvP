using UnityEngine;
using Ingame.towers;

namespace Interface.towers {
	public class TowerUI : MonoBehaviour {
		[SerializeField]
		private UIInformation[] _elements;

		public void Show(Tower tower) {
			if (tower is OffensiveTower) {
				OffensiveTower t = tower as OffensiveTower;

				_elements[0].gameObject.SetActive(true);
				_elements[0].Set("Damage", t.AttackDamage.ToString());

				_elements[1].gameObject.SetActive(true);
				_elements[1].Set("Attack Speed", t.AttackSpeed.ToString());

				_elements[2].gameObject.SetActive(true);
				_elements[2].Set("Range", t.Range.ToString());
			} else if (tower is BarracksTower) {
				BarracksTower t = tower as BarracksTower;

				_elements[0].gameObject.SetActive(true);
				_elements[0].Set("Respawn Time", t.RespawnTimer.ToString());

				_elements[1].gameObject.SetActive(false);
				_elements[2].gameObject.SetActive(false);
			} else {
				_elements[0].gameObject.SetActive(false);
				_elements[1].gameObject.SetActive(false);
				_elements[2].gameObject.SetActive(false);
			}
		}
	}
}
