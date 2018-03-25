using System;
using Server;
using UnityEngine;

public partial class GameServer {
	public void Register(string username, string displayName, string password, Action<bool> callback) {
		server.Register(username, displayName, password, callback);
	}
	public void Login(string username, string password, Action<UserProfile> callback) {
		server.Login(username, password, response => LoginCallback(response, callback));
	}
	public void Logout() {

	}

	private void LoginCallback(RequestResponse response, Action<UserProfile> callback) {
		if (!response.Success) {
			if (callback != null) callback(null);
			return;
		}

		if (callback != null) {
			ServerObject obj = response.ServerObject.GetServerObject("data");
			callback(JsonSerializer.Deserialize<UserProfile>(obj.JSON));
		}
	}
}
