using UnityEngine;
using Interface;

namespace GameEditor.Interface {
	public class MapSettingsPanel : MonoBehaviour {
		[SerializeField]
		private IntLabel _rowsLabel;
		[SerializeField]
		private IntLabel _columnsLabel;

		private GameEditorManager _gameManager;
		void Awake() {
			_gameManager = FindObjectOfType<GameEditorManager>();
		}

		private MapDescriptionEditor _mapDescription;
		public void Inject(MapDescriptionEditor mapDescription) {
			_mapDescription = mapDescription;

			_rowsLabel.value = _mapDescription.rows;
			_columnsLabel.value = _mapDescription.columns;

			_rowsLabel.OnChangeEvent += OnChangeEvent;
			_columnsLabel.OnChangeEvent += OnChangeEvent;
		}
		void OnDestroy() {
			if (_mapDescription != null) {
				_rowsLabel.OnChangeEvent -= OnChangeEvent;
				_columnsLabel.OnChangeEvent -= OnChangeEvent;
			}
		}

		private void OnChangeEvent() {
			_gameManager.ResizeMap(_rowsLabel.value, _columnsLabel.value);
		}
	}
}
