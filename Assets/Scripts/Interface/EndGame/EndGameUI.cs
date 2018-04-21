using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;

public class EndGameUI : MonoBehaviour {
	[SerializeField] private GameObject background;
	[SerializeField] private GameObject victoryEnabled;
	[SerializeField] private GameObject defeatEnabled;

	public void Show(bool win) {
		background.SetActive(true);

		victoryEnabled.SetActive(win);
		defeatEnabled.SetActive(!win);
	}
}
