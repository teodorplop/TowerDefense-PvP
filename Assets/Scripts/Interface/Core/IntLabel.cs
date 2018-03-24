using UnityEngine;

namespace Interface {
	public class IntLabel : Label {
		[SerializeField]
		private int _minimumValue = int.MinValue;
		[SerializeField]
		private int _maximumValue = int.MaxValue;

		public int value {
			get {
				int val;
				int.TryParse(label.text, out val);
				return val;
			}
			set {
				value = Mathf.Clamp(value, _minimumValue, _maximumValue);
				label.text = value.ToString();
			}
		}

		public override string text {
			get { return label.text; }
			set {
				int val;
				if (!int.TryParse(value, out val)) {
					Debug.LogError("Cannot set IntLabel " + name + " value to " + value, this);
				} else {
					this.value = val;
				}
			}
		}
	}
}
