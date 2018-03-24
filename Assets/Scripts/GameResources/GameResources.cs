using System;
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

public static partial class GameResources {
	private static IResourcesLoader projectResources = new ProjectResources();

	public static T LoadJSON<T>(string path) where T : IGameResource {
		return projectResources.LoadJSON<T>(path);
	}
	public static T LoadCSV<T>(string path) where T : CSVLoader, IGameResource {
		return projectResources.LoadCSV<T>(path);
	}
	public static T LoadFile<T>(string path) where T : ILoadableFile, new() {
		return projectResources.LoadFile<T>(path);
	}
}
