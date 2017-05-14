using UnityEngine;
using System;
using System.Collections;

public class StateMachineHandler : MonoBehaviour {
	private class GameState {
		public Func<IEnumerator> EnterState;
		public Func<IEnumerator> ExitState;

		public static IEnumerator DoNothing() { yield return null; }

		public GameState(Func<IEnumerator> enterState, Func<IEnumerator> exitState) {
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
	
	public void SetState(Enum state, StateMachineBase callingObject) {
		GameState oldGameState = _currentState;

		// Set object state to an empty one
		callingObject.currentState = null;
		_currentState = _emptyState;

		// Create the new state
		Func<IEnumerator> enterState = callingObject.ConfigureDelegate<Func<IEnumerator>>(state, "EnterState", GameState.DoNothing);
		Func<IEnumerator> exitState = callingObject.ConfigureDelegate<Func<IEnumerator>>(state, "ExitState", GameState.DoNothing);
		GameState newGameState = new GameState(enterState, exitState);

		StartCoroutine(ChangeState(state, callingObject, oldGameState, newGameState));
		// Exit old state
		oldGameState.ExitState();
		// Enter new state
		newGameState.EnterState();

		// Current state is now the new state
		callingObject.currentState = state;
		_currentState = newGameState;
	}

	private IEnumerator ChangeState(Enum state, StateMachineBase callingObject, GameState oldState, GameState newState) {
		yield return StartCoroutine(oldState.ExitState());
		yield return StartCoroutine(newState.EnterState());

		callingObject.currentState = state;
		_currentState = newState;
	}
}
