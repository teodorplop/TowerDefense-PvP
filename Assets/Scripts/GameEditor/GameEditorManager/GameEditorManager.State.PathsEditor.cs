using UnityEngine;

namespace GameEditor { 
	public partial class GameEditorManager {
		private void PathsEditor_HandleMouseUp(int mouseButton, Vector3 mousePosition) {
			if (_selectedPath == null) {
				return;
			}

			if (mouseButton == 1) {
				TileEditorRenderer tileRenderer = InputScanner.ScanFor<TileEditorRenderer>(mousePosition, mapLayer);
				if (tileRenderer != null && tileRenderer.Tile.tileType == TileType.Reachable) {
					Vector2i position = new Vector2(tileRenderer.transform.localPosition.z, tileRenderer.transform.localPosition.x);
					if (_selectedPath.AddPoint(position)) {
						_pathRenderer.DisplayPath(_selectedPath);
					}
				}
			}
		}
		private void PathsEditor_HandleKey() {
			if (Input.GetKeyUp(KeyCode.Delete)) {
				TileEditorRenderer tileRenderer = InputScanner.ScanFor<TileEditorRenderer>(Input.mousePosition, mapLayer);
				if (tileRenderer != null) {
					Vector2i position = new Vector2(tileRenderer.transform.localPosition.z, tileRenderer.transform.localPosition.x);
					if (_selectedPath.RemovePoint(position)) {
						_pathRenderer.DisplayPath(_selectedPath);
					}
				}
			}
		}

		private PathDescriptionEditor _selectedPath;
		public void SetSelectedPath(string pathName) {
			if (string.IsNullOrEmpty(pathName)) {
				_selectedPath = null;
				_pathRenderer.DisplayPath(_selectedPath);
			} else {
				PathDescriptionEditor path = _mapDescription.GetPath(pathName) as PathDescriptionEditor;
				_selectedPath = path;
				_pathRenderer.DisplayPath(_selectedPath);
			}
		}
	}
}
