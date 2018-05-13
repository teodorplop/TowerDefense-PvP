using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;

public class EndGameUI : MonoBehaviour {
	[SerializeField] private GameObject background;
	[SerializeField] private GameObject victoryEnabled;
	[SerializeField] private GameObject defeatEnabled;

	[SerializeField] private Label mmrLabel;
	[SerializeField] private Label mmrDiffLabel;

	private int oldMMR;

	void Awake() {
		EventManager.AddListener<ProfileChangedEvent>(OnProfileChanged);
	}
	void OnDestroy() {
		EventManager.AddListener<ProfileChangedEvent>(OnProfileChanged);
	}

	public void Show(int mmr, bool win) {
		oldMMR = mmr;

		background.SetActive(true);

		mmrLabel.text = oldMMR.ToString();
		victoryEnabled.SetActive(win);
		defeatEnabled.SetActive(!win);
	}

	void OnProfileChanged(ProfileChangedEvent evt) {
		int newMMR = evt.Profile.MMR;
		if (oldMMR > newMMR)
			mmrDiffLabel.text = "-" + (oldMMR - newMMR);
		else
			mmrDiffLabel.text = "+" + (newMMR - oldMMR);
	}
}
