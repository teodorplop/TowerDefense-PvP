using UnityEngine;
using TMPro;

namespace GameEditor.Interface {
	public class SaveSettingsPanel : MonoBehaviour {
		[SerializeField]
		private TMP_InputField _mapNameInputField;
		
		private MapDescription _mapDescription;
		public void Inject(MapDescription mapDescription) {
			_mapDescription = mapDescription;
		}
		
		public void Save() {
			string mapName = _mapNameInputField.text;
			if (_mapDescription != null && !string.IsNullOrEmpty(mapName)) {
				GameResources.Save(_mapDescription, "Maps/" + mapName);
			}
		}
	}
}