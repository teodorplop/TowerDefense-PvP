using UnityEngine;

namespace Interface.units {
	public class HealthBarUI : MonoBehaviour {
		[SerializeField]
		private RectTransform _healthBar;
		[SerializeField]
		private Label _healthLabel;

		private float _healthBarWidth;
		private BaseUnit _target;
		void Awake() {
			_healthBarWidth = _healthBar.rect.width;
		}
		public void Inject(BaseUnit target) {
			_target = target;
		}

		void LateUpdate() {
			if (_target == null) {
				return;
			}
			
			float healthPercent = (float)_target.CurrentHealth / _target.MaxHealth;
			_healthBar.sizeDelta = new Vector2((healthPercent - 1.0f) * _healthBarWidth, 0);

			_healthLabel.text = _target.CurrentHealth.ToString() + " / " + _target.MaxHealth;
		}
	}
}
