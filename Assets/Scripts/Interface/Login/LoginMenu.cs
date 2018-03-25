using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginMenu : UIMenuPanel {
	[SerializeField] private TMP_InputField usernameField;
	[SerializeField] private TMP_InputField passwordField;
	[SerializeField] private Toggle rememberMeToggle;

	void Awake() {
		rememberMeToggle.onValueChanged.AddListener(delegate(bool value) {
			UserPrefs.RememberMe = value;
		});
	}
	void Start() {
		if (rememberMeToggle.isOn = UserPrefs.RememberMe) {
			usernameField.text = UserPrefs.Username;
			passwordField.text = UserPrefs.Password;
		}
	}

	public void Login() {
		App.Login(usernameField.text, passwordField.text);
	}
}
