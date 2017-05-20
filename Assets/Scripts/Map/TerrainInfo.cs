using UnityEngine;

public class TerrainInfo : MonoBehaviour {
	private Terrain _terrain;
	private int _alphamapWidth, _alphamapHeight;
	private float[,,] _splatmapData;
	private int _numTextures;

	[SerializeField]
	private int[] _movementPenalties;

	void Awake() {
		_terrain = FindObjectOfType<Terrain>();

		_alphamapWidth = _terrain.terrainData.alphamapWidth;
		_alphamapHeight = _terrain.terrainData.alphamapHeight;

		_splatmapData = _terrain.terrainData.GetAlphamaps(0, 0, _alphamapWidth, _alphamapHeight);
		_numTextures = _splatmapData.Length / (_alphamapWidth * _alphamapHeight);

		if (_numTextures > _movementPenalties.Length) {
			Debug.LogWarning("We have not set movement penalties for all textures used in the terrain.", gameObject);
		} else if (_numTextures < _movementPenalties.Length) {
			Debug.LogWarning("We have set more movement penalties than textures used in the terrain.", gameObject);
		}
	}

	private Vector3 ConvertToSplatMapCoordinate(Vector3 position) {
		Vector3 vecRet = Vector3.zero;
		Vector3 terPosition = transform.position;
		vecRet.x = ((position.x - terPosition.x) / _terrain.terrainData.size.x) * _terrain.terrainData.alphamapWidth;
		vecRet.z = ((position.z - terPosition.z) / _terrain.terrainData.size.z) * _terrain.terrainData.alphamapHeight;
		return vecRet;
	}

	private int GetActiveTerrainTextureIdx(Vector3 position) {
		Vector3 terrainCord = ConvertToSplatMapCoordinate(position);
		int ret = 0;
		float comp = 0f;
		for (int i = 0; i < _numTextures; i++) {
			if (comp < _splatmapData[(int)terrainCord.z, (int)terrainCord.x, i])
				ret = i;
		}
		return ret;
	}
	
	public Vector3 WorldSize {
		get { return _terrain.terrainData.size; }
	}

	public Rect WorldRectangle {
		get { return new Rect(transform.position, new Vector2(WorldSize.x, WorldSize.z)); }
	}

	public int GetMovementPenalty(Vector3 worldPosition) {
		return _movementPenalties[GetActiveTerrainTextureIdx(worldPosition)];
	}
}
