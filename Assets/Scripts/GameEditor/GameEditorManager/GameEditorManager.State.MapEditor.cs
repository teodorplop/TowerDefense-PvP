using UnityEngine;

namespace GameEditor {
	public partial class GameEditorManager {
		private void MapEditor_HandleMouseDown(int mouseButton, Vector3 mousePosition) {
			TileEditorRenderer tileEditorRenderer = InputScanner.ScanFor<TileEditorRenderer>(mousePosition, mapLayer);
			if (tileEditorRenderer != null) {
				tileEditorRenderer.SetTileType();
			}
		}
		private void MapEditor_HandleMouse(int mouseButton, Vector3 mousePosition) {
			TileEditorRenderer tileEditorRenderer = InputScanner.ScanFor<TileEditorRenderer>(mousePosition, mapLayer);
			if (tileEditorRenderer != null) {
				tileEditorRenderer.SetTileType();
			}
		}

		public void ResizeMap(int rows, int columns) {
			_mapDescription.Resize(rows, columns);
			_mapRenderer.OnMapResized();
		}
	}
}
