using UnityEngine;
using UnityEngine.UI;
using Ingame.towers;

namespace Interface.towers {
	public class TowerUI : MonoBehaviour {
		[SerializeField] private GameObject _properties;
		[SerializeField] private Label _attackDamage;
		[SerializeField] private Label _attackSpeed;
		[SerializeField] private Label _range;
		[SerializeField] private Label _respawnTime;

		public void Show(Tower tower) {
			if (tower == null) {
				_properties.SetActive(false);
				return;
			}

			if (tower is OffensiveTower) {
				OffensiveTower t = tower as OffensiveTower;

				_attackDamage.text = t.AttackDamage.ToString();
				_attackDamage.transform.parent.gameObject.SetActive(true);

				_attackSpeed.text = t.AttackSpeed.ToString();
				_attackSpeed.transform.parent.gameObject.SetActive(true);

				_range.text = t.Range.ToString();
				_range.transform.parent.gameObject.SetActive(true);

				_respawnTime.transform.parent.gameObject.SetActive(false);
			} else if (tower is BarracksTower) {
				BarracksTower t = tower as BarracksTower;

				_respawnTime.text = t.RespawnTimer.ToString();
				_respawnTime.transform.parent.gameObject.SetActive(true);

				_attackDamage.transform.parent.gameObject.SetActive(false);
				_attackSpeed.transform.parent.gameObject.SetActive(false);
				_range.transform.parent.gameObject.SetActive(false);
			} else {
				_attackDamage.transform.parent.gameObject.SetActive(false);
				_attackSpeed.transform.parent.gameObject.SetActive(false);
				_range.transform.parent.gameObject.SetActive(false);
				_respawnTime.transform.parent.gameObject.SetActive(false);
			}

			_properties.SetActive(true);
		}
	}
}
