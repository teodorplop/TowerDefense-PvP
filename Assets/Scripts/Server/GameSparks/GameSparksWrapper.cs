using System;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Api.Messages;
using GameSparks.Core;
using UnityEngine;

namespace Server {
	/// <summary>
	/// Wrapper for GameSparks specific server.
	/// </summary>
	public class GameSparksWrapper : ServerWrapper {
		public GameSparksWrapper() {
			ScriptMessage.Listener += OnMessageReceived;
		}

		public override void Register(string username, string displayName, string password, Action<bool> callback) {
			new RegistrationRequest().SetUserName(username).SetDisplayName(displayName).SetPassword(password).Send(response => RegisterCallback(response, callback));
		}

		private void RegisterCallback(RegistrationResponse response, Action<bool> callback) {
			if (response.HasErrors) {
				Debug.LogWarning("Server error: " + response.Errors.JSON);
			}
			if (callback != null) {
				callback(!response.HasErrors);
			}
		}

		public override void Login(string username, string password, Action<RequestResponse> callback) {
			new AuthenticationRequest().SetUserName(username).SetPassword(password).Send(response => LoginCallback(response, callback));
		}

		private void LoginCallback(AuthenticationResponse response, Action<RequestResponse> callback) {
			if (response.HasErrors) {
				Debug.LogWarning("Server error: " + response.Errors.JSON);
			}
			if (callback != null) {
				ServerObject serverObject = new GameSparksServerObject(response.ScriptData);
				RequestResponse requestResponse = new RequestResponse(!response.HasErrors, response.JSONString, serverObject);
				callback(requestResponse);
			}
		}

		public override void SendRequest(string requestName, Action<RequestResponse> callback) {
			new LogEventRequest().SetEventKey(requestName).Send(response => SendRequestCallback(response, callback));
		}

		public override void SendRequest(string requestName, string json, Action<RequestResponse> callback) {
			GSRequestData gsData = new GSRequestData(json);
			new LogEventRequest().SetEventKey(requestName).SetEventAttribute("data", gsData).Send(response => SendRequestCallback(response, callback));
		}

		private void SendRequestCallback(LogEventResponse response, Action<RequestResponse> callback) {
			if (response.HasErrors) {
				Debug.LogWarning("Server error: " + response.Errors.JSON);
			}
			if (callback != null) {
				ServerObject serverObject = new GameSparksServerObject(response.ScriptData);
				RequestResponse requestResponse = new RequestResponse(!response.HasErrors, response.JSONString, serverObject);
				callback(requestResponse);
			}
		}

		private void OnMessageReceived(ScriptMessage message) {
			if (message.HasErrors) {
				Debug.LogWarning("Server error: " + message.Errors.JSON);
			}

			ServerObject serverObject = new GameSparksServerObject(message.Data);
			ServerMessage serverMessage = new ServerMessage(message.Title, message.SubTitle, message.HasErrors, message.JSONString, serverObject);
			if (onServerMessage != null) {
				onServerMessage(serverMessage);
			}
		}
	}
}
