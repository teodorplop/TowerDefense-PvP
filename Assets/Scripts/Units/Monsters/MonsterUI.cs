using UnityEngine;
using Interface;

public class MonsterUI : MonoBehaviour {
	[SerializeField]
	private Vector3 _offsetToMonster;
	[SerializeField]
	private RectTransform _healthBar;
	[SerializeField]
	private Label _label;

	private float _healthBarWidth;
	private Monster _target;
	public void Inject(Monster monster) {
		_target = monster;
		_healthBarWidth = _healthBar.rect.width;
	}

	void LateUpdate() {
		if (_target == null) {
			return;
		}

		transform.position = _target.transform.position + _offsetToMonster;
		float healthPercent = (float)_target.CurrentHealth / _target.MaxHealth;
		_healthBar.sizeDelta = new Vector2((healthPercent - 1.0f) * _healthBarWidth, 0);

		if (_target.DebugOn) {
			_label.text = _target.currentState == null ? "Null" : _target.currentState.ToString();
		}
	}
}
