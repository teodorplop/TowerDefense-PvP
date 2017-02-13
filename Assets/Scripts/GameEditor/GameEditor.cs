using UnityEngine;

public class GameEditor : MonoBehaviour {
	[SerializeField]
	private int _mapRows;
	[SerializeField]
	private int _mapColumns;

	private MapEditor _mapEditor;
	void Start() {
		_mapEditor = new MapEditor(_mapRows, _mapColumns);
	}

	public void MapRows(int add) {
		if (_mapRows + add > 0) {
			_mapRows += add;
			_mapEditor.Resize(_mapRows, _mapColumns);
		}
	}
	public void MapColumns(int add) {
		if (_mapColumns + add > 0) {
			_mapColumns += add;
			_mapEditor.Resize(_mapRows, _mapColumns);
		}
	}
}
