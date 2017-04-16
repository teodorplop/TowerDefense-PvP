using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using Interface;

public partial class GameManager : MonoBehaviour {
	[SerializeField]
	private string _defaultMap;

	private MapDescription _mapDescription;
	void Start() {
		if (SceneManager.sceneCount == 1) {
			StartCoroutine(Initialize());
		}
	}
	IEnumerator Initialize() {
		GameResources.LoadAll();

		AsyncOperation uiScene = SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
		while (!uiScene.isDone) {
			yield return null;
		}

		TextAsset mapAsset = Resources.Load<TextAsset>(_defaultMap);
		Initialize(JsonSerializer.Deserialize<MapDescription>(mapAsset.text));
		Resources.UnloadAsset(mapAsset);
	}

	public void Initialize(MapDescription mapDescription) {
		_mapDescription = mapDescription;

		FindObjectOfType<MapRenderer>().Initialize(_mapDescription);

		//InputContext context = new InputContext(OnMouseDown, OnMouseUp, OnMouse);
		//FindObjectOfType<InputManager>().PushContext(context);

		EventManager.AddListener<TowerConstructionEvent>(OnTowerConstruction);

		InitializeUI();
	}

	private FillablePanel _towersPanel;
	private void InitializeUI() {
		// TODO: here get towers for this particular map only
		List<TowerBase> towers = new List<TowerBase>();
		towers.Add(GameResources.Load<TowerBase>("Towers/TowerCanon"));
		towers.Add(GameResources.Load<TowerBase>("Towers/TowerMG"));

		_towersPanel = GameObject.Find("Towers Panel").GetComponent<FillablePanel>();
		_towersPanel.Populate(towers);
	}
}
