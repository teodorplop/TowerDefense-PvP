using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {
	public static string ActiveScene { get { return SceneManager.GetActiveScene().name; } }

	public static void LoadScene(string name) {
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
	}
	public static void LoadSceneAdditive(string name) {
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
	}
}
