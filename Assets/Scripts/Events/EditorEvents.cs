
namespace GameEditor {
	public class EditorStateChangedEvent : GameEvent {
		private GameEditorManager.GameEditorState _state;
		public GameEditorManager.GameEditorState State { get { return _state; } }
		public EditorStateChangedEvent(GameEditorManager.GameEditorState state) {
			_state = state;
		}
	}
}
