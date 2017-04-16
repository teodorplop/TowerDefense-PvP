using UnityEngine;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour {
	[SerializeField]
	protected TileRenderer _prefab;

	protected MapDescription _mapDescription;
	protected List<List<TileRenderer>> _tiles;

	public virtual void Initialize(MapDescription mapDescription) {
		_mapDescription = mapDescription;
		_tiles = new List<List<TileRenderer>>();

		Render();
	}

	private void Render() {
		for (int i = 0; i < _mapDescription.rows; ++i) {
			_tiles.Add(new List<TileRenderer>());
			for (int j = 0; j < _mapDescription.columns; ++j) {
				TileRenderer tr = InstantiateTileRenderer(_mapDescription[i, j], i, j);
				_tiles[i].Add(tr);
			}
		}
	}
	private Vector3 GetPositionForTile(int row, int column) {
		return new Vector3(column, 0, row);
	}
	protected TileRenderer InstantiateTileRenderer(TileDescription tile, int row, int column) {
		TileRenderer tr = Instantiate(_prefab);
		tr.transform.SetParent(transform);
		tr.transform.localPosition = GetPositionForTile(row, column);
		tr.gameObject.SetActive(true);
		tr.SetTile(tile);
		return tr;
	}
}
