using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using GameEditor.Interface;

namespace GameEditor {
	public class GameEditorManager : MonoBehaviour {
		[SerializeField]
		private int _mapRows;
		[SerializeField]
		private int _mapColumns;
		
		private MapDescriptionEditor _mapDescription;
		void Start() {
			_mapDescription = new MapDescriptionEditor(_mapRows, _mapColumns);
			FindObjectOfType<MapEditorRenderer>().Initialize(_mapDescription);
			if (SceneManager.sceneCount == 1) {
				StartCoroutine(Initialize());
			} else {
				InitializeUI();
			}
		}

		void InitializeUI() {
			FindObjectOfType<MapSettingsPanel>().Initialize(_mapDescription);
			FindObjectOfType<SaveSettingsPanel>().Initialize(_mapDescription);
		}
		IEnumerator Initialize() {
			GameResources.LoadAll();

			AsyncOperation uiScene = SceneManager.LoadSceneAsync("EditorUI", LoadSceneMode.Additive);
			while (!uiScene.isDone) {
				yield return null;
			}

			InitializeUI();
		}
	}
}
