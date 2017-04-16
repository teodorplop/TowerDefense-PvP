using UnityEngine;

namespace GameEditor.Interface {
	public class EditorUIManager : MonoBehaviour {
		public void Inject(GameEditorManager gameManager, MapDescriptionEditor mapDescription) {
			GetComponentInChildren<MapSettingsPanel>(true).Inject(gameManager, mapDescription);
			GetComponentInChildren<PathsEditorPanel>(true).Inject(gameManager, mapDescription);
			GetComponentInChildren<SaveSettingsPanel>(true).Inject(mapDescription);
		}
	}
}
