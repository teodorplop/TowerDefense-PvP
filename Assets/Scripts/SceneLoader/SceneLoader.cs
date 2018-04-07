using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public static class SceneLoader {
	public static string ActiveScene { get { return SceneManager.GetActiveScene().name; } }

	public static void LoadScene(string name, Action callback = null) {
		CoroutineStarter.CoroutineStart(LoadSceneCoroutine(name, LoadSceneMode.Single, callback));
	}
	public static void LoadSceneAdditive(string name, Action callback = null) {
		CoroutineStarter.CoroutineStart(LoadSceneCoroutine(name, LoadSceneMode.Additive, callback));
	}
	public static void UnloadScene(string name, Action callback = null) {
		CoroutineStarter.CoroutineStart(UnloadSceneCoroutine(name, callback));
	}
	public static void SetActiveScene(Scene scene) {
		SceneManager.SetActiveScene(scene);
	}

	private static IEnumerator LoadSceneCoroutine(string name, LoadSceneMode mode, Action callback) {
		AsyncOperation load = SceneManager.LoadSceneAsync(name, mode);
		while (!load.isDone) yield return null;
		if (callback != null) callback();
	}
	private static IEnumerator UnloadSceneCoroutine(string name, Action callback) {
		AsyncOperation unload = SceneManager.UnloadSceneAsync(name);
		while (!unload.isDone) yield return null;
		if (callback != null) callback();
	}
}
