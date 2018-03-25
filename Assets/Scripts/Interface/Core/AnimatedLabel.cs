using UnityEngine;
using Interface;
using TMPro;

namespace Interface {
	[RequireComponent(typeof(TMP_Text))]
	public class AnimatedLabel : MonoBehaviour {
		[SerializeField]
		private float _delay = .5f;

		private Label _label;
		private TMP_Text _tmpLabel;
		private float _timer;

		public string text {
			get { return _label.text; }
			set { _label.text = value; }
		}

		void Awake() {
			_label = GetComponent<Label>();
			_tmpLabel = GetComponent<TMP_Text>();
		}

		void Update() {
			_timer += Time.deltaTime;

			if (_timer >= _delay) {
				_timer = 0.0f;

				string text = _tmpLabel.text;
				if (text.EndsWith("...")) text = text.Remove(text.Length - 3);
				else text += '.';
				_tmpLabel.text = text;
			}
		}
	}
}