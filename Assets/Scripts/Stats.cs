using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Stats {
	[SerializeField]
	private Hashtable _table = new Hashtable();
	
	public T GetStat<T>(string stat) {
		return _table[stat] == null ? default(T) : (T)_table[stat];
	}
}
