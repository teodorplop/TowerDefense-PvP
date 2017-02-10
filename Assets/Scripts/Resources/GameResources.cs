using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static partial class GameResources {
	private static string _resourcesRoot = Path.Combine(Application.streamingAssetsPath, "GameResources");
	private static string _extension = ".json";
	private static Dictionary<string, object> _resources = new Dictionary<string, object>();

	/// <summary>
	/// Dereferences all resources. Does not truly UNLOAD them, as there's no such thing in C# :(
	/// </summary>
	public static void UnloadAll() {
		_resources.Clear();
	}

	/// <summary>
	/// Returns resource at path as an object
	/// </summary>
	public static object Load(string path) {
		path = Hash(path);

		if (!_resources.ContainsKey(path)) {
			Debug.LogError("No resource loaded at " + path + ". Try using Load<T> instead.");
			return null;
		}
		return _resources[path];
	}

	/// <summary>
	/// Returns T resource at path
	/// </summary>
	public static T Load<T>(string path) {
		path = Hash(path);

		T resource = default(T);
		if (_resources.ContainsKey(path)) {
			resource = (T)_resources[path];
		} else {
			resource = LoadResource<T>(path);
		}
		return resource;
	}

	/// <summary>
	/// Loads an object at path.
	/// </summary>
	private static T LoadResource<T>(string path) {
		if (!Path.HasExtension(path)) {
			path += _extension;
		}

		T resource = default(T);
		if (File.Exists(path)) {
			resource = DataSerializer.DeserializeData<T>(path);

			path = path.Remove(0, _resourcesRoot.Length + 1);
			path = path.Remove(path.Length - _extension.Length, _extension.Length);
			_resources.Add(Hash(path), resource);
		} else {
			Debug.LogError("No file found at " + path);
		}
		return resource;
	}

	/// <summary>
	/// Returns an universal path, with \ instead of /.
	/// </summary>
	private static string Hash(string path) {
		return path.Replace('/', '\\');
	}
}
