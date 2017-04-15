using UnityEngine;
using System;

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

	private GameState _currentState;
	void Awake() {
		_currentState = new GameState();
	}
	
	/// <summary>
	/// Changes the state of a certain state machine.
	/// </summary>
	public void SetState(Enum state, StateMachineBase callingObject) {
		// Configure new state of the object
		callingObject.currentState = state;

		Action enterState = callingObject.ConfigureDelegate<Action>("EnterState", GameState.DoNothing);
		Action exitState = callingObject.ConfigureDelegate<Action>("ExitState", GameState.DoNothing);

		// Create the new state
		GameState newGameState = new GameState(enterState, exitState);

		// Exit old state
		_currentState.ExitState();
		// Enter new state
		newGameState.EnterState();
		// Current state is now the new state
		_currentState = newGameState;
	}
}
