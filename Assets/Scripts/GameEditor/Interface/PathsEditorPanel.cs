using System.Collections.Generic;
using UnityEngine;
using Interface;

namespace GameEditor.Interface {
	public class PathsEditorPanel : MonoBehaviour {
		[SerializeField]
		private PathButtonUI _pathPrefab;
		[SerializeField]
		private TabGroup _pathsTabGroup;

		private int _pathsIndex;
		private List<PathButtonUI> _paths;
		void Awake() {
			_pathPrefab.gameObject.SetActive(false);
			_pathsIndex = 0;
			_paths = new List<PathButtonUI>();
		}

		public void Inject(MapDescriptionEditor mapDescription) {

		}

		public void OnNewPath() {
			PathButtonUI newPath = Instantiate(_pathPrefab);
			newPath.transform.SetParent(_pathsTabGroup.transform);
			newPath.transform.SetSiblingIndex(_pathsTabGroup.transform.childCount - 2);
			newPath.gameObject.SetActive(true);

			newPath.PathName = "Path" + _pathsIndex;
			_pathsTabGroup.AddTab(newPath.Tab);
			_paths.Add(newPath);

			++_pathsIndex;
		}

		public void OnDeletePath(PathButtonUI pathButton) {
			_pathsTabGroup.RemoveTab(pathButton.Tab);
			_paths.Remove(pathButton);

			Destroy(pathButton.gameObject);
		}
	}
}
