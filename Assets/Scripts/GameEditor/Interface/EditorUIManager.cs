using UnityEngine;

namespace GameEditor.Interface {
	public class EditorUIManager : MonoBehaviour {
		public void Inject(MapDescriptionEditor mapDescription) {
			GetComponentInChildren<MapSettingsPanel>(true).Inject(mapDescription);
			GetComponentInChildren<PathsEditorPanel>(true).Inject(mapDescription);
			GetComponentInChildren<SaveSettingsPanel>(true).Inject(mapDescription);
		}
	}
}
