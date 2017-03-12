using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interface {
	public class SceneLoaderBehaviour : MonoBehaviour {
		public void LoadScene(string scene) {
			SceneManager.LoadScene(scene);
		}
	}
}