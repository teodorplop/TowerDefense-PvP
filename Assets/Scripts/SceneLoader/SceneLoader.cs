using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {
	public static string ActiveScene { get { return SceneManager.GetActiveScene().name; } }


}
