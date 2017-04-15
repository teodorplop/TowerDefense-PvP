using UnityEngine;
using System;

namespace GameEditor.Interface {
	public class EditorStatePanel : MonoBehaviour {
		public void ChangeEditorState(string state) {
			object gameState = Enum.Parse(typeof(GameEditorManager.GameEditorState), state);
			if (gameState == null) {
				Debug.LogError("No GameEditorState found for " + state);
			} else {
				EventManager.Raise(new EditorStateChangedEvent((GameEditorManager.GameEditorState)gameState));
			}
		}
	}
}
