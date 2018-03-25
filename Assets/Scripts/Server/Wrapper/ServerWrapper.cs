using System;

namespace Server {
	/// <summary>
	/// Base class for a general server provider.
	/// </summary>
	public abstract class ServerWrapper {
		public abstract void Register(string username, string displayName, string password, Action<bool> callback);
		public abstract void Login(string username, string password, Action<RequestResponse> callback);
		public abstract void SendRequest(string requestName, Action<RequestResponse> callback);
		public abstract void SendRequest(string requestName, string json, Action<RequestResponse> callback);

		protected Action<ServerMessage> onServerMessage;
		public void SetListener(Action<ServerMessage> listener) {
			onServerMessage = listener;
		}
	}
}
