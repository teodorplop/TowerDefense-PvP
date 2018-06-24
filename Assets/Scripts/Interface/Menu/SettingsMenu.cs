using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {
	public void MainMenu() {
		EventManager.Raise(new ExitEvent());
		SceneLoader.LoadScene("Menu");
	}

	public void Exit() {
		EventManager.Raise(new ExitEvent());
		App.Exit();
	}

	public void Options() {

	}

	public void Logout() {
		App.Logout();
	}
}
