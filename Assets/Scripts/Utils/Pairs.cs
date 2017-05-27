using UnityEngine;
using System;

[Serializable]
public struct Pair<T, U> {
	[SerializeField]
	public T first;
	[SerializeField]
	public U second;

	public Pair(T first = default(T), U second = default(U)) {
		this.first = first;
		this.second = second;
	}
}
