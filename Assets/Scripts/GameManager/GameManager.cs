using System;
using UnityEngine;
using Ingame.waves;
using Ingame.towers;

public partial class GameManager : StateMachineBase {
	public enum GameState {
		None,
		Loading,
		Idle,
		TowerSelected
	}

	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

	[SerializeField]
	private LayerMask _towerMask;
	[SerializeField]
	private float _gridNodeRadius = 0.75f;
	[SerializeField]
	private int _gridBlurSize = 3;
	[SerializeField]
	private Wallet _wallet;
	
	private UIManager _uiManager;
	private InputManager _inputManager;
	private WavesManager _wavesManager;
	private TowerFactory _towerFactory;
	private RequestDispatcher _dispatcher;

	protected new void Awake() {
		base.Awake();

		_instance = this;
		_uiManager = FindObjectOfType<UIManager>();
		_inputManager = FindObjectOfType<InputManager>();
		_wavesManager = FindObjectOfType<WavesManager>();
		_towerFactory = FindObjectOfType<TowerFactory>();

		_dispatcher = new RequestDispatcher();

		InitializeHandlers();
	}
	void OnDestroy() {
		Players.OnDestroy();
		_instance = null;
	}

	void Start() {
		SetState(GameState.Loading);
	}

	private void SetState(GameState state) {
		if (currentState == null || (GameState)currentState != state) {
			_inputManager.PopContext();
			_dispatcher.SetHandlers(None_ActionHandlers);

			_stateMachineHandler.SetState(state, this);

			// These should be performed only after we entered state. But meh... Wharever.
			_inputManager.PushContext(GenerateInputContext(state));
			_dispatcher.SetHandlers(GetHandlers(state));
		}
	}

	private InputContext GenerateInputContext(GameState state) {
		InputContext input = new InputContext();

		input.onMouseDown = ConfigureDelegate<Action<int, Vector3>>(state, "HandleMouseDown", None_HandleMouseDown);
		input.onMouseUp = ConfigureDelegate<Action<int, Vector3>>(state, "HandleMouseUp", None_HandleMouseUp);
		input.onMouse = ConfigureDelegate<Action<Vector3>>(state, "HandleMouse", None_HandleMouse);
		input.onKey = ConfigureDelegate<Action>(state, "HandleKey", None_HandleKey);

		return input;
	}

	private ActionHandler[] GetHandlers(GameState state) {
		return ConfigureField(state, "ActionHandlers", None_ActionHandlers);
	}

	private void InitializeHandlers() {
		ActionHandler upgradeTower = new UpgradeTowerHandler();
		ActionHandler sellTower = new SellTowerHandler();
		
		TowerSelected_ActionHandlers = new ActionHandler[] { upgradeTower, sellTower };
	}
}
