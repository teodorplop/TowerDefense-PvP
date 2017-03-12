
public class GameSplash : SplashSceneLoader {
	void Start() {
		GameResources.LoadAll();
		StartCoroutine(LoadScenes());
	}

	protected override void AfterLoad() {

	}
}
