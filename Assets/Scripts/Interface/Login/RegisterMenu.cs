using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterMenu : UIMenuPanel {
	[SerializeField] private TMP_InputField usernameField;
	[SerializeField] private TMP_InputField emailField;
	[SerializeField] private TMP_InputField passwordField;

	public void Register() {
		App.Register(usernameField.text, emailField.text, passwordField.text);
	}
}
