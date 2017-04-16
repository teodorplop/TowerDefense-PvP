using System.Collections.Generic;
using UnityEngine;
using Interface;

namespace GameEditor.Interface {
	public class PathsEditorPanel : MonoBehaviour {
		[SerializeField]
		private PathButtonUI _pathPrefab;
		[SerializeField]
		private TabGroup _pathsTabGroup;

		private GameEditorManager _gameManager;
		private int _pathsIndex;
		private List<PathButtonUI> _paths;
		void Awake() {
			_pathPrefab.gameObject.SetActive(false);
			_pathsIndex = 0;
			_paths = new List<PathButtonUI>();
			_gameManager = FindObjectOfType<GameEditorManager>();
			_pathsTabGroup.OnSelectionChangedEvent += OnSelectedPathChangedEvent;
		}

		private MapDescriptionEditor _mapDescription;
		public void Inject(MapDescriptionEditor mapDescription) {
			_mapDescription = mapDescription;
		}

		public void OnNewPath() {
			PathButtonUI newPath = Instantiate(_pathPrefab);
			newPath.transform.SetParent(_pathsTabGroup.transform);
			newPath.transform.SetSiblingIndex(_pathsTabGroup.transform.childCount - 2);
			newPath.gameObject.SetActive(true);

			string pathName = "Path" + _pathsIndex;
			newPath.PathName = pathName;
			_mapDescription.AddPath(pathName);

			_pathsTabGroup.AddTab(newPath.Tab);
			_paths.Add(newPath);

			++_pathsIndex;
		}

		public void OnDeletePath(PathButtonUI pathButton) {
			_pathsTabGroup.RemoveTab(pathButton.Tab);
			_paths.Remove(pathButton);

			_mapDescription.RemovePath(pathButton.PathName);

			Destroy(pathButton.gameObject);
		}

		private void OnSelectedPathChangedEvent(Tab tab) {
			string path = tab.GetComponent<PathButtonUI>().PathName;
			_gameManager.SetSelectedPath(path);
		}
	}
}
