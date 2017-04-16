using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace GameEditor {
	public class PathEditorRenderer : MonoBehaviour {
		[SerializeField]
		private TextMeshPro _prefab;

		private MapEditorRenderer _mapRenderer;
		private List<TextMeshPro> _pathObjects;
		void Awake() {
			_mapRenderer = FindObjectOfType<MapEditorRenderer>();
			_pathObjects = new List<TextMeshPro>();

			_prefab.gameObject.SetActive(false);
		}

		private void Resize(int count) {
			if (_pathObjects.Count > count) {
				for (int i = count; i < _pathObjects.Count; ++i) {
					Destroy(_pathObjects[i].gameObject);
				}
				_pathObjects.RemoveRange(count, _pathObjects.Count - count);
			} else {
				while (count > _pathObjects.Count) {
					TextMeshPro obj = Instantiate(_prefab);
					obj.transform.SetParent(transform);
					obj.gameObject.SetActive(true);

					_pathObjects.Add(obj);
				}
			}
		}

		public void DisplayPath(PathDescriptionEditor path) {
			List<Vector2i> points = path.Points;

			Resize(points.Count);
			for (int i = 0; i < points.Count; ++i) {
				_pathObjects[i].text = i.ToString();
				_pathObjects[i].transform.localPosition = new Vector3(points[i].y, 0.15f, points[i].x);
			}
		}
	}
}
