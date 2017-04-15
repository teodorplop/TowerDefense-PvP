using UnityEngine;

namespace GameEditor {
	public partial class GameEditorManager {
		private void MapEditor_EnterState() {
		}
		private void MapEditor_ExitState() {
		}

		private void MapEditor_HandleMouseDown(Vector3 mousePosition) {
			TileEditorRenderer tileEditorRenderer = InputScanner.ScanFor<TileEditorRenderer>(mousePosition, mapLayer);
			if (tileEditorRenderer != null) {
				tileEditorRenderer.SetTileType();
			}
		}
		private void MapEditor_HandleMouse(Vector3 mousePosition) {
			TileEditorRenderer tileEditorRenderer = InputScanner.ScanFor<TileEditorRenderer>(mousePosition, mapLayer);
			if (tileEditorRenderer != null) {
				tileEditorRenderer.SetTileType();
			}
		}
		private void MapEditor_HandleMouseUp(Vector3 mousePosition) {
			 
		}
	}
}
