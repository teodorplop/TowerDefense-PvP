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
				int.TryParse(base.text, out val);
				return val;
			}
			set {
				value = Mathf.Clamp(value, _minimumValue, _maximumValue);
				base.text = value.ToString();
			}
		}

		public override string text {
			get {
				return base.text;
			}
			set {
				int val;
				if (!int.TryParse(value, out val)) {
					Debug.LogError("Cannot set IntLabel " + name + " value to " + value);
				} else {
					this.value = val;
				}
			}
		}

		public void Add(int add) {
			value += add;
		}
	}
}
