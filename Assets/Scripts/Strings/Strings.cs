using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Strings_Data : IGameResource {
	public string Name { get; set; }

	[SerializeField]
	private Hashtable hashtable;

	public string this[string id] {
		get {
			object obj = hashtable[id];
			return obj == null ? id : obj as string;
		}
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
}
