using UnityEngine;
using Interface;

public class UnitUI : MonoBehaviour {
	[SerializeField]
	private Vector3 _offsetToUnit;
	[SerializeField]
	private RectTransform _healthBar;
	[SerializeField]
	private Label _label;

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

		transform.position = _target.transform.position + _offsetToUnit;
		float healthPercent = (float)_target.CurrentHealth / _target.MaxHealth;
		_healthBar.sizeDelta = new Vector2((healthPercent - 1.0f) * _healthBarWidth, 0);

		if (_target.DebugOn) {
			_label.text = _target.currentState == null ? "Null" : _target.currentState.ToString();
		}
	}
}
