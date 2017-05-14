﻿using System.Collections.Generic;
using UnityEngine;

public class PathsContainer : MonoBehaviour {
	private Dictionary<string, Vector3[]> _paths;
	void Awake() {
		_paths = new Dictionary<string, Vector3[]>();
		foreach (Transform t in transform) {
			List<Vector3> path = new List<Vector3>();
			foreach (Transform tChild in t.transform) {
				path.Add(tChild.position);
			}
			
			_paths.Add(t.name, path.ToArray());
		}
	}

	public Vector3[] GetPath(string name) {
		if (!_paths.ContainsKey(name)) {
			Debug.LogError(name + " path does not exist.");
			return null;
		}
		return _paths[name];
	}
}