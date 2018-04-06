using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {
	public void Exit() {
		App.Exit();
	}

	public void Options() {

	}

	public void Logout() {
		App.Logout();
	}
}
