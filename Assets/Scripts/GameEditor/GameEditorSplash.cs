using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameEditor {
	public class GameEditorSplash : MonoBehaviour {
		void Start() {
			StartCoroutine(LoadScenes());
		}

		IEnumerator LoadScenes() {
			AsyncOperation uiScene = SceneManager.LoadSceneAsync("EditorUI", LoadSceneMode.Additive);
			AsyncOperation worldScene = SceneManager.LoadSceneAsync("EditorWorld", LoadSceneMode.Additive);

			while (!uiScene.isDone || !worldScene.isDone) {
				yield return null;
			}

			AsyncOperation unloadSplash = SceneManager.UnloadSceneAsync("EditorSplash");
			while (!unloadSplash.isDone) {
				yield return null;
			}
		}
	}
}
