﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using GameEditor.Interface;
using System;

namespace GameEditor {
	public partial class GameEditorManager : StateMachineBase {
		[SerializeField]
		private LayerMask mapLayer;

		public enum GameEditorState {
			None,
			MapEditor,
			PathsEditor
		}

		[SerializeField]
		private int _mapRows;
		[SerializeField]
		private int _mapColumns;

		private InputManager _inputManager;
		private MapEditorRenderer _mapRenderer;
		private MapDescriptionEditor _mapDescription;
		void Start() {
			_inputManager = FindObjectOfType<InputManager>();
			_mapRenderer = FindObjectOfType<MapEditorRenderer>();
			_mapDescription = new MapDescriptionEditor(_mapRows, _mapColumns);

			SetState(GameEditorState.None);
			_mapRenderer.Initialize(_mapDescription);

			if (SceneManager.sceneCount == 1) {
				StartCoroutine(Initialize());
			} else {
				Initialized();
			}

			EventManager.AddListener<EditorStateChangedEvent>(OnEditorStateChanged);
		}
		void OnDestroy() {
			EventManager.RemoveListener<EditorStateChangedEvent>(OnEditorStateChanged);
		}
		private void OnEditorStateChanged(EditorStateChangedEvent evt) {
			SetState(evt.State);
		}

		private IEnumerator Initialize() {
			GameResources.LoadAll();

			AsyncOperation uiScene = SceneManager.LoadSceneAsync("EditorUI", LoadSceneMode.Additive);
			while (!uiScene.isDone) {
				yield return null;
			}

			Initialized();
		}

		private void Initialized() {
			// Initialize UI
			FindObjectOfType<EditorUIManager>().Inject(_mapDescription);

			// Initialize state machine
			SetState(GameEditorState.MapEditor);
		}

		private void SetState(GameEditorState state) {
			if (currentState != null && (GameEditorState)currentState == state) {
				return;
			}

			// remove old state input context
			_inputManager.PopContext();

			_stateMachineHandler.SetState(state, this);

			// push new state input context
			_inputManager.PushContext(GenerateInputContext(state));
		}

		private InputContext GenerateInputContext(GameEditorState state) {
			Action<Vector3> onMouseDown = ConfigureDelegate<Action<Vector3>>(state, "HandleMouseDown", None_HandleMouseDown);
			Action<Vector3> onMouseUp = ConfigureDelegate<Action<Vector3>>(state, "HandleMouseUp", None_HandleMouseUp);
			Action<Vector3> onMouse = ConfigureDelegate<Action<Vector3>>(state, "HandleMouse", None_HandleMouse);
			return new InputContext(onMouseDown, onMouseUp, onMouse);
		}
	}
}
