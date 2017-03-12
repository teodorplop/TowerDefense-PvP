using TMPro;
using UnityEngine;

namespace Interface {
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class Label : MonoBehaviour {
		protected TextMeshProUGUI _label;
		protected void Awake() {
			_label = GetComponent<TextMeshProUGUI>();
		}

		public delegate void OnChangeHandler();
		public event OnChangeHandler OnChangeEvent;

		public virtual string text {
			get { return _label.text; }
			set {
				_label.text = value;
				if (OnChangeEvent != null) {
					OnChangeEvent();
				}
			}
		}
	}
}
