using System;
using UnityEngine;

[Serializable]
public class Wallet {
	public enum Currency { Health, Gold }

	[SerializeField, Header("Health, Gold")]
	private int[] _values = new int[2];

	public bool Check(Currency currency, int value) {
		return _values[Convert.ToInt32(currency)] >= value;
	}
	public int Get(Currency currency) {
		return _values[Convert.ToInt32(currency)];
	}
	public void Add(Currency currency, int value) {
		_values[Convert.ToInt32(currency)] += value;
	}
	public void Subtract(Currency currency, int value) {
		if (!Check(currency, value)) {
			Debug.LogError("Not enough " + currency);
			return;
		}
		_values[Convert.ToInt32(currency)] -= value;
	}

	public Wallet Clone() {
		Wallet clone = new Wallet();
		_values.CopyTo(clone._values, 0);

		return clone;
	}
}
