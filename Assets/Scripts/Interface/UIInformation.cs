using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface {
	public class UIInformation : MonoBehaviour {
		[SerializeField]
		private Label _titleLabel;
		[SerializeField]
		private Label _label;

		public void Set(string title, string value) {
			_titleLabel.text = title;
			_label.text = value;
		}
	}
}
