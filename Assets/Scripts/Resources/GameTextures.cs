using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameTextures : MonoBehaviour {
	private static string texturesRoot { get { return Path.Combine(Application.streamingAssetsPath, "GameTextures"); } }

	private static string _extension = ".png";
	private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
	private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

	public static void LoadAll() {

	}
	public static Texture2D LoadTexture(string path) {
		path = Hash(path);

		if (!_textures.ContainsKey(path)) {
			Debug.LogError("No texture loaded at " + path);
			return new Texture2D(16, 16);
		}
		return _textures[path];
	}
	public static Sprite LoadSprite(string path) {
		path = Hash(path);

		if (!_sprites.ContainsKey(path)) {
			if (!_textures.ContainsKey(path)) {
				Debug.LogError("No texture loaded at " + path);
				return new Sprite();
			}
			_sprites.Add(path, CreateSprite(_textures[path]));
		}
		return _sprites[path];
	}
	private static Sprite CreateSprite(Texture2D texture) {
		return Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), 
			new Vector2(0.5f, 0.5f));
	}

	IEnumerator Load(string path) {
		path = Hash(path);

		WWW www = new WWW(path);//"File://" + texturesRoot + "/TowerCanon.png");
		while (!www.isDone) {
			yield return null;
		}

		Debug.Log("WWW Error: " + www.error);
		Texture2D texture = www.texture;
		texture.Compress(false);

		_textures.Add(path, texture);
	}

	/// <summary>
	/// Compares two paths
	/// </summary>
	private static bool PathsAreEqual(string path1, string path2) {
		return path1.Replace('/', '\\') == path2.Replace('/', '\\');
	}

	/// <summary>
	/// Returns full given path.
	/// </summary>
	private static string FullPath(string path) {
		if (path.Length < texturesRoot.Length || !PathsAreEqual(path.Substring(0, texturesRoot.Length), texturesRoot)) {
			path = Path.Combine(texturesRoot, path);
		}
		if (!Path.HasExtension(path)) {
			path += _extension;
		}
		return path;
	}

	/// <summary>
	/// Returns hash equal with relative path.
	/// </summary>
	private static string Hash(string path) {
		path = path.Replace('/', '\\');
		if (path.Length >= texturesRoot.Length && PathsAreEqual(path.Substring(0, texturesRoot.Length), texturesRoot)) {
			path = path.Remove(0, texturesRoot.Length + 1);
		}
		if (path.Length >= _extension.Length &&
			path.Substring(path.Length - _extension.Length, _extension.Length) == _extension) {
			path = path.Remove(path.Length - _extension.Length, _extension.Length);
		}

		return path;
	}
}
