using UnityEngine;
using TMPro;

namespace GameEditor.Interface {
	public class SaveSettingsPanel : MonoBehaviour {
		[SerializeField]
		private TMP_InputField _mapNameInputField;

		private bool _initialized;
		private MapDescription _mapDescription;
		public void Initialize(MapDescription mapDescription) {
			_mapDescription = mapDescription;
			_initialized = true;
		}
		
		public void Save() {
			string mapName = _mapNameInputField.text;
			if (_initialized && !string.IsNullOrEmpty(mapName)) {
				GameResources.Save(_mapDescription, "Maps/" + mapName);
			}
		}
	}
}