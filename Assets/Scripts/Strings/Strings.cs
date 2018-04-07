using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Strings_Data : IGameResource {
	public string Name { get; set; }

	[SerializeField]
	private Dictionary<string, string> hashtable;

	public string this[string id] {
		get {
			string value = null;
			return hashtable.TryGetValue(id, out value) ? value : id;
		}
	}

	public bool HasText(string id) {
		return hashtable.ContainsKey(id);
	}
}

public class Strings {
    private static Strings instance;
    private static Strings Instance {
        get { return instance ?? (instance = new Strings("English")); }
    }

	private Strings_Data container;
    private Strings(string language) {
		container = GameResources.LoadStrings(language);
    }

    public static string GetText(string id) {
		return Instance.container[id];
    }

	public static bool HasText(string id) {
		return Instance.container.HasText(id);
	}
}
