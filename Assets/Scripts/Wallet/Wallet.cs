using System;
using UnityEngine;

[Serializable]
public class Wallet {
	public enum Currency { Health, Gold }

	[SerializeField, Header("Health, Gold")]
	private int[] values = new int[2];

	public bool Check(Currency currency, int value) {
		return values[Convert.ToInt32(currency)] >= value;
	}
	public int Get(Currency currency) {
		return values[Convert.ToInt32(currency)];
	}
	public void Add(Currency currency, int value) {
		values[Convert.ToInt32(currency)] += value;
	}
	public void Subtract(Currency currency, int value) {
		if (!Check(currency, value)) {
			Debug.LogError("Not enough " + currency);
			return;
		}
		values[Convert.ToInt32(currency)] -= value;
	}

	public static Wallet Clone(Wallet target) {
		Wallet wallet = new Wallet();
		target.values.CopyTo(wallet.values, 0);

		return wallet;
	}
}
