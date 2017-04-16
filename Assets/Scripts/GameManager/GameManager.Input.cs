using UnityEngine;

public partial class GameManager {
	private TileRenderer _selected;
	private void SelectTile(TileRenderer target) {
		if (_selected != null) {
			_selected.Select(false);
			_selected = null;
		}
		_selected = target;
		if (_selected != null) {
			_selected.Select(true);
			_towersPanel.gameObject.SetActive(true);
		} else {
			_towersPanel.gameObject.SetActive(false);
		}
	}
	private void OnTowerConstruction(TowerConstructionEvent evt) {
		if (_selected != null) {
			TowerFactory.Instantiate(evt.target.name, _selected.transform.position);
			SelectTile(null);
		}
	}

	private void OnMouseDown(int mouseButton, Vector3 mousePosition) {
		TileRenderer target = InputScanner.ScanFor<TileRenderer>(mousePosition, 1 << 8);
		if (target != null && target.Tile.tileType == TileType.Constructable) {
			SelectTile(target);
		}
	}
	private void OnMouse(int mouseButton, Vector3 mousePosition) {
		TileRenderer target = InputScanner.ScanFor<TileRenderer>(mousePosition, 1 << 8);
		if (target != _selected) {
			SelectTile(null);
		}
	}
	private void OnMouseUp(int mouseButton, Vector3 mousePosition) {
		TileRenderer target = InputScanner.ScanFor<TileRenderer>(mousePosition, 1 << 8);
		if (target != _selected) {
			SelectTile(null);
		}
	}
}
