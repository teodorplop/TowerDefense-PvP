using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[SerializeField]
	private string _defaultMap;

	private MapDescription _mapDescription;
	void Start() {
		if (SceneManager.sceneCount == 1) {
			TextAsset mapAsset = Resources.Load<TextAsset>(_defaultMap);
			Initialize(JsonSerializer.Deserialize<MapDescription>(mapAsset.text));
			Resources.UnloadAsset(mapAsset);
		}
	}
	public void Initialize(MapDescription mapDescription) {
		_mapDescription = mapDescription;
		FindObjectOfType<MapRenderer>().Initialize(_mapDescription);
	}
}
