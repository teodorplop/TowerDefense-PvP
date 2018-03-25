using UnityEngine;
using Server;
using System;

public partial class GameServer {
	private ServerWrapper server;

	public GameServer() {
		server = new GameSparksWrapper();
		server.SetListener(OnServerMessage);
	}

	private void OnServerMessage(ServerMessage message) {
		if (message.HasErrors) {
			Debug.LogError("ServerMessage error: " + message.JsonString);
			return;
		}

		Debug.Log(message);

		string typeName = message.ServerObject.GetString("$type");
		Type type = Type.GetType(typeName);

		if (type != null) {
			GameEvent evt = JsonSerializer.Deserialize<GameEvent>(message.ServerObject.JSON);
			EventManager.Raise(evt);
		} else {
			Debug.LogError(typeName + " type for server message not found.");
		}
	}
}
