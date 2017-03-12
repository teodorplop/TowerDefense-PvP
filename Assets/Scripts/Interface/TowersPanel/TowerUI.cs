using UnityEngine;
using UnityEngine.UI;

namespace Interface {
	public class TowerUI : FillableItem {
		private Image _image;
		private TowerBase _tower;
		public TowerBase tower { get { return _tower; } }
		void Awake() {
			_image = GetComponent<Image>();
		}

		public override void Inject(object obj) {
			_tower = obj as TowerBase;
			_image.sprite = Resources.Load<Sprite>("TowerUI/" + tower.name);
		}

		public void OnPress() {
			EventManager.Instance.Raise(new TowerConstructionEvent(_tower));
		}
	}
}
