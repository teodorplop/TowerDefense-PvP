using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.units {
	public class UnitUI : MonoBehaviour {
		[SerializeField] private HealthBarUI _healthBar;
		[SerializeField] private GameObject _properties;
		[SerializeField] private Label _attackDamage;
		[SerializeField] private Label _attackSpeed;
		[SerializeField] private Label _movementSpeed;
		[SerializeField] private Label _lifePenalty;

		public void Show(BaseUnit unit) {
			if (unit == null) {
				_healthBar.gameObject.SetActive(false);
				_properties.SetActive(false);
				return;
			}

			_healthBar.gameObject.SetActive(true);
			_healthBar.Inject(unit);

			_attackDamage.text = unit.AttackDamage.ToString();
			_attackSpeed.text = unit.AttackSpeed.ToString();
			_movementSpeed.text = unit.MovementSpeed.ToString();

			if (unit is Monster) {
				_lifePenalty.text = (unit as Monster).LifeTaken.ToString();
				_lifePenalty.transform.parent.gameObject.SetActive(true);
			} else {
				_lifePenalty.transform.parent.gameObject.SetActive(false);
			}

			_properties.SetActive(true);
		}
	}
}
