using System;
using System.Collections;
using UnityEngine;

public class Strings {
	[Serializable]
	private class Container : IGameResource {
		public string Name { get; set; }

		[SerializeField] private Hashtable hashtable;

		public string this[string id] {
			get {
				object obj = hashtable[id];
				return obj == null ? id : obj as string;
			}
		}
	}

    private static Strings instance;
    private static Strings Instance {
        get { return instance ?? (instance = new Strings("English")); }
    }

	private Container container;
    private Strings(string file) {
		container = GameResources.LoadJSON<Container>(file);
    }

    public static string GetText(string id) {
		return Instance.container[id];
    }
}
