using UnityEngine;

public static partial class GameResources {
	private static T Load<T>(string path) {
		TextAsset asset = Resources.Load<TextAsset>(path);
		if (asset == null) {
			Debug.LogError("Load(" + path + ") failed.");
			return default(T);
		}

		T resource = JsonSerializer.Deserialize<T>(asset.text);
		Resources.UnloadAsset(asset);
		return resource;
	}

	private static T Load<T>(string level, string folder, string name) {
		TextAsset asset = Resources.Load<TextAsset>(folder + '/' + level + '/' + name);
		if (asset == null) {
			asset = Resources.Load<TextAsset>(folder + '/' + name);
		}

		if (asset == null) {
			Debug.LogError("Load(" + level + ", " + folder + ", " + name + ") failed.");
			return default(T);
		}

		T resource = JsonSerializer.Deserialize<T>(asset.text);
		Resources.UnloadAsset(asset);
		return resource;
	}

	private static T Load<T>(string folder, string name) {
		return Load<T>(SceneLoader.ActiveScene, folder, name);
	}
}
