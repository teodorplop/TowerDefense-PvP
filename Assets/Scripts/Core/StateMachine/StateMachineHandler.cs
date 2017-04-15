using UnityEngine;
using System;

/// <summary>
/// StateMachine with instant transitions. Override SetState of this to be able to animate transitions.
/// </summary>
public class StateMachineHandler : MonoBehaviour {
	private class GameState {
		public Action EnterState;
		public Action ExitState;

		public static void DoNothing() {}

		public GameState(Action enterState, Action exitState) {
			EnterState = enterState;
			ExitState = exitState;
		}
		public GameState() {
			EnterState = DoNothing;
			ExitState = DoNothing;
		}
	}

	private GameState _emptyState, _currentState;
	void Awake() {
		_emptyState = new GameState();
		_currentState = new GameState();
	}
	
	/// <summary>
	/// Changes the state of a certain state machine.
	/// </summary>
	public virtual void SetState(Enum state, StateMachineBase callingObject) {
		GameState oldGameState = _currentState;

		// Set object state to an empty one
		callingObject.currentState = null;
		_currentState = _emptyState;

		// Create the new state
		Action enterState = callingObject.ConfigureDelegate<Action>(state, "EnterState", GameState.DoNothing);
		Action exitState = callingObject.ConfigureDelegate<Action>(state, "ExitState", GameState.DoNothing);
		GameState newGameState = new GameState(enterState, exitState);

		// Exit old state
		oldGameState.ExitState();
		// Enter new state
		newGameState.EnterState();

		// Current state is now the new state
		callingObject.currentState = state;
		_currentState = newGameState;
	}
}
