using UnityEngine;
using System.Collections.Generic;

namespace Interface {
	public class FillablePanel : MonoBehaviour {
		[SerializeField]
		protected FillableItem _prefab;
		private List<FillableItem> _items;
		void Awake() {
			_items = new List<FillableItem>();
		}

		private void ClearContent() {
			foreach (FillableItem towerUI in _items) {
				Destroy(towerUI.gameObject);
			}
			_items.Clear();
		}

		public void Populate<T>(List<T> items) {
			ClearContent();

			for (int i = 0; i < items.Count; ++i) {
				FillableItem itemUI = Instantiate(_prefab);
				itemUI.transform.SetParent(transform);
				itemUI.gameObject.SetActive(true);
				_items.Add(itemUI);

				itemUI.Inject(items[i]);
			}
		}
	}
}
