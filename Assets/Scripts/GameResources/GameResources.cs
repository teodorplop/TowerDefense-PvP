using System;
using System.Collections;
using System.IO;
using UnityEngine;

public interface IGameResource {
	string Name { get; set; }
}

public interface ILoadableFile : IGameResource {
	void Load(string text);
}

public interface IResourcesLoader {
	T LoadJSON<T>(string path) where T : IGameResource;
	T LoadCSV<T>(string path) where T : CSVLoader, IGameResource;
	T LoadFile<T>(string path) where T : ILoadableFile, new();
}

public class ProjectResources : IResourcesLoader {
	public T LoadJSON<T>(string path) where T : IGameResource {
		TextAsset asset = Resources.Load<TextAsset>(path);
		if (asset == null) {
			Debug.LogWarning("LoadJSON(" + path + ") failed.");
			return default(T);
		}

		T resource = JsonSerializer.Deserialize<T>(asset.text);
		resource.Name = path;
		Resources.UnloadAsset(asset);
		return resource;
	}

	public T LoadCSV<T>(string path) where T : CSVLoader, IGameResource {
		TextAsset asset = Resources.Load<TextAsset>(path);
		if (asset == null) {
			Debug.LogWarning("LoadCSV(" + path + ") failed.");
			return default(T);
		}

		T resource = Activator.CreateInstance(typeof(T), asset.text) as T;
		resource.Name = path;
		Resources.UnloadAsset(asset);
		return resource;
	}

	public T LoadFile<T>(string path) where T : ILoadableFile, new() {
		TextAsset asset = Resources.Load<TextAsset>(path);
		if (asset == null) {
			Debug.LogWarning("LoadFile(" + path + ") failed.");
			return default(T);
		}

		T resource = new T();
		resource.Name = path;
		resource.Load(asset.text);
		return resource;
	}
}

public class ExternalResources : IResourcesLoader {
	public T LoadJSON<T>(string path) where T : IGameResource {
		string fullPath = Path.Combine(Application.streamingAssetsPath, path + ".txt");
		if (!File.Exists(fullPath)) {
			Debug.LogWarning("LoadFile(" + path + ") failed. Full path: " + fullPath);
			return default(T);
		}

		T resource = JsonSerializer.Deserialize<T>(File.ReadAllText(fullPath));
		resource.Name = Path.GetFileNameWithoutExtension(path);
		return resource;
	}

	public T LoadCSV<T>(string path) where T : CSVLoader, IGameResource {
		string fullPath = Path.Combine(Application.streamingAssetsPath, path + ".txt");
		if (!File.Exists(fullPath)) {
			Debug.LogWarning("LoadCSV(" + path + ") failed. Full path: " + fullPath);
			return default(T);
		}

		T resource = Activator.CreateInstance(typeof(T), File.ReadAllText(fullPath)) as T;
		resource.Name = Path.GetFileNameWithoutExtension(path);
		return resource;
	}

	public T LoadFile<T>(string path) where T : ILoadableFile, new() {
		string fullPath = Path.Combine(Application.streamingAssetsPath, path + ".txt");
		if (!File.Exists(fullPath)) {
			Debug.LogWarning("LoadFile(" + path + ") failed. Full path: " + fullPath);
			return default(T);
		}

		T resource = new T();
		resource.Name = Path.GetFileNameWithoutExtension(path);
		resource.Load(File.ReadAllText(fullPath));
		return resource;
	}
}

public static partial class GameResources {
	private static IResourcesLoader projectResources = new ProjectResources();
	private static IResourcesLoader externalResources = new ExternalResources();

	public static T LoadJSON<T>(string path) where T : IGameResource {
		return externalResources.LoadJSON<T>(path);
	}
	public static T LoadCSV<T>(string path) where T : CSVLoader, IGameResource {
		return externalResources.LoadCSV<T>(path);
	}
	public static T LoadFile<T>(string path) where T : ILoadableFile, new() {
		return externalResources.LoadFile<T>(path);
	}
}
