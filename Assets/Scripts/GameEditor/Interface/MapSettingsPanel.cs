using UnityEngine;
using Interface;

namespace GameEditor.Interface {
	public class MapSettingsPanel : MonoBehaviour {
		[SerializeField]
		private IntLabel _rowsLabel;
		[SerializeField]
		private IntLabel _columnsLabel;

		private bool _initialized;
		private MapDescriptionEditor _mapDescription;
		public void Inject(MapDescriptionEditor mapDescription) {
			_mapDescription = mapDescription;

			_rowsLabel.value = _mapDescription.rows;
			_columnsLabel.value = _mapDescription.columns;

			_rowsLabel.OnChangeEvent += OnChangeEvent;
			_columnsLabel.OnChangeEvent += OnChangeEvent;

			_initialized = true;
		}
		void OnDestroy() {
			if (_initialized) {
				_rowsLabel.OnChangeEvent -= OnChangeEvent;
				_columnsLabel.OnChangeEvent -= OnChangeEvent;
			}
		}

		private void OnChangeEvent() {
			_mapDescription.Resize(_rowsLabel.value, _columnsLabel.value);
		}
	}
}
