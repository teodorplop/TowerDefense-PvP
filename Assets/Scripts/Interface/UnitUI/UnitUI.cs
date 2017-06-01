using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface.units {
	public class UnitUI : MonoBehaviour {
		[SerializeField]
		private HealthBarUI _healthBar;
		[SerializeField]
		private UIInformation[] _elements;

		public void Show(BaseUnit unit) {
			if (unit == null) {
				_healthBar.gameObject.SetActive(false);
				for (int i = 0; i < _elements.Length; ++i) {
					_elements[i].gameObject.SetActive(false);
				}
				return;
			}

			_healthBar.gameObject.SetActive(true);
			_healthBar.Inject(unit);

			_elements[0].gameObject.SetActive(true);
			_elements[0].Set("Movement Speed", unit.MovementSpeed.ToString());

			_elements[1].gameObject.SetActive(true);
			_elements[1].Set("Attack Speed", unit.AttackSpeed.ToString());

			_elements[2].gameObject.SetActive(true);
			_elements[2].Set("Range", unit.AttackRange.ToString());
		}
	}
}
