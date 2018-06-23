using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public class Mod {
	private string name;
	public string Name { get { return name; } }

	private string hash;
	public string Hash { get { return hash; } }

	public Mod(string path) {
		name = Path.GetFileNameWithoutExtension(path);
		hash = CalculateHash(path);
	}

	private string CalculateHash(string path) {
		string hash = null;
		using (MD5 md5Hash = MD5.Create()) {
			hash = GetMd5Hash(md5Hash, GetAllContent(path));
		}

		return hash;
	}

	private static string GetAllContent(string path) {
		StringBuilder sb = new StringBuilder();

		DirectoryInfo dir = new DirectoryInfo(path);
		foreach (var file in dir.GetFiles("*", SearchOption.AllDirectories)) {
			if (file.Extension == ".txt") {
				sb.Append(file.FullName);
				sb.Append(File.ReadAllText(file.FullName));
			}
		}

		return sb.ToString();
	}

	private static string GetMd5Hash(MD5 md5Hash, string input) {
		// Convert the input string to a byte array and compute the hash.
		byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

		// Create a new Stringbuilder to collect the bytes
		// and create a string.
		StringBuilder sBuilder = new StringBuilder();

		// Loop through each byte of the hashed data 
		// and format each one as a hexadecimal string.
		for (int i = 0; i < data.Length; i++) {
			sBuilder.Append(data[i].ToString("x2"));
		}

		// Return the hexadecimal string.
		return sBuilder.ToString();
	}
}

public class Modding : SingletonComponent<Modding> {
	private List<Mod> mods;

	public Mod[] Mods { get { return mods.ToArray(); } }

	void Start() {
		DontDestroyOnLoad(this);
		PopulateMods();
	}

	private void PopulateMods() {
		DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath);
		DirectoryInfo[] directories = dirInfo.GetDirectories();
		mods = new List<Mod>();
		foreach (DirectoryInfo dir in directories)
			mods.Add(ReadMod(dir));

		int defaultIdx = mods.FindIndex(obj => obj.Name == "Default");
		if (defaultIdx == -1) {
			Debug.LogError("Default mod missing.");
			return;
		}

		if (mods.Count > 1) {
			Mod aux = mods[0];
			aux = mods[defaultIdx];
			mods[defaultIdx] = aux;
		}
	}

	private Mod ReadMod(DirectoryInfo dir) {
		Mod mod = new Mod(dir.FullName);
		return mod;
	}
}
