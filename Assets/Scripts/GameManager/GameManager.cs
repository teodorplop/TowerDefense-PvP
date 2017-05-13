using System;
using UnityEngine;
using Pathfinding;

public partial class GameManager : StateMachineBase {
	public enum GameState {
		None,
		Idle,
		ConstructionSelected,
		TowerSelected
	}

	[SerializeField]
	private float _gridNodeRadius;

	private InputManager _inputManager;
	private TerrainInfo _terrain;
	private Grid _grid;
	private Pathfinder _pathfinder;
	protected new void Awake() {
		base.Awake();

		_inputManager = FindObjectOfType<InputManager>();
		_terrain = FindObjectOfType<TerrainInfo>();
	}

	void Start() {
		_grid = new Grid(_gridNodeRadius, _terrain);
		_pathfinder = new Pathfinder(_grid);

		SetState(GameState.Idle);
	}

	private void SetState(GameState state) {
		if (currentState == null || (GameState)currentState != state) {
			_inputManager.PopContext();

			_stateMachineHandler.SetState(state, this);

			_inputManager.PushContext(GenerateInputContext(state));
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
}
