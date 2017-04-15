using UnityEngine;

public class TileRenderer : MonoBehaviour {
	[SerializeField]
	private Color _constructableColor;
	[SerializeField]
	private Color _environmentColor;
	[SerializeField]
	private Color _reachableColor;
	[SerializeField]
	protected Color _highlightedColor;

	protected MeshRenderer _renderer;
	protected Color _color;
	protected TileDescription _tile;
	public TileDescription Tile { get { return _tile; } }
	void Awake() {
		_renderer = GetComponent<MeshRenderer>();
	}

	public void SetTile(TileDescription target) {
		_tile = target;

		if (target.tileType == TileType.Constructable) {
			_color = _renderer.material.color = _constructableColor;
		} else if (target.tileType == TileType.Environment) {
			_color = _renderer.material.color = _environmentColor;
		} else if (target.tileType == TileType.Reachable) {
			_color = _renderer.material.color = _reachableColor;
		}
	}

	public void Select(bool selected) {
		_renderer.material.color = selected ? _highlightedColor : _color;
	}
}
