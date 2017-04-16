using UnityEngine;
using Interface;

namespace GameEditor.Interface {
	public class PathButtonUI : MonoBehaviour {
		[SerializeField]
		private Label _nameLabel;
		public string PathName {
			get { return _nameLabel.text; }
			set { _nameLabel.text = value; }
		}

		private Tab tab;
		public Tab Tab { get { return tab; } }
		void Awake() {
			tab = GetComponent<Tab>();
		}
	}
}
