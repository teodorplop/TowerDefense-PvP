using UnityEngine;

public class TileRenderer : MonoBehaviour {
	protected MeshRenderer _renderer;
	protected Color _color;
	protected Tile _target;
	void Awake() {
		_renderer = GetComponent<MeshRenderer>();
	}

	public void SetTile(Tile target) {
		_target = target;

		if (target.tileType == TileType.Constructable) {
			_color = _renderer.material.color = Color.green;
		} else if (target.tileType == TileType.Environment) {
			_color = _renderer.material.color = Color.black;
		} else if (target.tileType == TileType.Path) {
			_color = _renderer.material.color = Color.white;
		}
	}
}
