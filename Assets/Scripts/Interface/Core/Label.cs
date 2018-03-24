using TMPro;
using UnityEngine;

namespace Interface {
	[RequireComponent(typeof(TMP_Text))]
	public class Label : MonoBehaviour {
		private TMP_Text _label;
		protected TMP_Text label {  get { return _label ?? (_label = GetComponent<TMP_Text>()); } }

		public virtual string text {
			get { return label.text; }
			set { label.text = value; }
		}
	}
}
