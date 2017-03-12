using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameEditor {
	public class GameEditorSplash : SplashSceneLoader {
		void Start() {
			StartCoroutine(LoadScenes());
		}

		protected override void AfterLoad() {
		}
	}
}
