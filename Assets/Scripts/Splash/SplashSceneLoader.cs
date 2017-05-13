using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashSceneLoader : MonoBehaviour {
	[SerializeField]
	private string _uiScene;
	[SerializeField]
	private string _worldScene;

	void Start() {
		StartCoroutine(LoadScenes());
	}

	protected IEnumerator LoadScenes() {
		AsyncOperation uiScene = SceneManager.LoadSceneAsync(_uiScene, LoadSceneMode.Additive);
		AsyncOperation worldScene = SceneManager.LoadSceneAsync(_worldScene, LoadSceneMode.Additive);

		while (!uiScene.isDone || !worldScene.isDone) {
			yield return null;
		}

		AfterLoad();

		AsyncOperation unloadSplash = SceneManager.UnloadSceneAsync(gameObject.scene);
		while (!unloadSplash.isDone) {
			yield return null;
		}
	}

	protected virtual void AfterLoad() {}
}
