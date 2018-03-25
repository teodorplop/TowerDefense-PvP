using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserPrefs {
	public static string Username {
		get { return PlayerPrefs.GetString("Username"); }
		set { PlayerPrefs.SetString("Username", value); PlayerPrefs.Save(); }
	}
	public static string Password {
		get { return PlayerPrefs.GetString("Password"); }
		set { PlayerPrefs.SetString("Password", value); PlayerPrefs.Save(); }
	}
	public static bool RememberMe {
		get { return PlayerPrefs.GetInt("RememberMe") == 1; }
		set { PlayerPrefs.SetInt("RememberMe", value ? 1 : 0); PlayerPrefs.Save(); }
	}
}
