using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StreamingAssets {
	private const string DEFAULT_MOD_PATH = "Default";
	private const string TEXTURES_PATH = "Textures";

	private static bool _loaded = false;
	private static Sprite _defaultSprite = new Sprite();
	private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

	public static void UnloadAll() {
		foreach (var entry in _sprites)
			Sprite.Destroy(entry.Value);
		_sprites.Clear();
	}

	public static void LoadTextures(Action callback) {
		if (!_loaded) {
			_loaded = true;
			CoroutineStarter.CoroutineStart(LoadAllTextures(callback));
		} else if (callback != null)
			callback();
	}

	public static Sprite GetSprite(string name) {
		Sprite sprite;
		return _sprites.TryGetValue(name, out sprite) ? sprite : _defaultSprite;
	}

	private static IEnumerator LoadAllTextures(Action callback) {
		string path = Path.Combine(Application.streamingAssetsPath, DEFAULT_MOD_PATH);
		path = Path.Combine(path, TEXTURES_PATH);

		DirectoryInfo dirInfo = new DirectoryInfo(path);
		FileInfo[] texFiles = dirInfo.GetFiles("*.png", SearchOption.AllDirectories);

		foreach (FileInfo file in texFiles)
			yield return CoroutineStarter.CoroutineStart(LoadTexture(file));

		if (callback != null) callback();
	}

	private static IEnumerator LoadTexture(FileInfo file) {
		WWW www = new WWW(file.FullName);
		yield return www;

		string spriteName = Path.GetFileNameWithoutExtension(file.Name);
		Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(.5f, .5f));
		_sprites.Add(spriteName, sprite);

		yield return null;
	}
}
