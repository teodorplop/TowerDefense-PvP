using System;
using UnityEngine;
using Pathfinding;

public partial class GameManager : StateMachineBase {
	public enum GameState {
		None,
		Idle,
		TowerSelected
	}

	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

	[SerializeField]
	private LayerMask _towerMask;
	[SerializeField]
	private float _gridNodeRadius;

	private UIManager _uiManager;
	private InputManager _inputManager;
	private TerrainInfo _terrain;
	private TowerFactory _towerFactory;

	private RequestDispatcher _dispatcher;
	private Grid _grid;
	private Pathfinder _pathfinder;
	protected new void Awake() {
		base.Awake();

		_instance = this;
		_uiManager = FindObjectOfType<UIManager>();
		_inputManager = FindObjectOfType<InputManager>();
		_terrain = FindObjectOfType<TerrainInfo>();
		_towerFactory = FindObjectOfType<TowerFactory>();

		InitializeHandlers();
	}

	void Start() {
		_dispatcher = new RequestDispatcher();
		_grid = new Grid(_gridNodeRadius, _terrain);
		_pathfinder = new Pathfinder(_grid);

		SetState(GameState.Idle);
	}

	private void SetState(GameState state) {
		if (currentState == null || (GameState)currentState != state) {
			_inputManager.PopContext();

			_stateMachineHandler.SetState(state, this);

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

		Idle_ActionHandlers = new ActionHandler[] { upgradeTower, sellTower };
	}
}
