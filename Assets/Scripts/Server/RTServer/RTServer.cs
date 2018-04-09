using UnityEngine;
using GameSparks.Core;
using GameSparks.Api.Responses;
using GameSparks.RT;
using System;

public class RTServer {
	private MatchInfo matchInfo;
	private Action<bool> onReady;

	public int PeerId { get { return (int)GameSparksRTUnity.Instance.PeerId; } }

	public RTServer(MatchInfo matchInfo) {
		this.matchInfo = matchInfo;
	}

	public void SetOnReady(Action<bool> onReady) {
		this.onReady = onReady;
	}

	public void Connect() {
		GSRequestData mockedResponse = new GSRequestData().
			AddNumber("port", (double)matchInfo.PortId).
			AddString("host", matchInfo.HostURL).
			AddString("accessToken", matchInfo.AccessToken);

		FindMatchResponse response = new FindMatchResponse(mockedResponse);

		GameSparksRTUnity.Instance.Configure(response, OnPlayerConnect, OnPlayerDisconnect, OnReady, OnPacket);
		GameSparksRTUnity.Instance.Connect();
	}

	private bool PlayersOnline() {
		return matchInfo.Players.Find(obj => !obj.online) == null;
	}

	private void OnPlayerConnect(int peerId) {
		Debug.Log("OnPlayerConnect(" + peerId + ")");
		matchInfo.GetPlayer(peerId).online = true;
	}
	private void OnPlayerDisconnect(int peerId) {
		Debug.Log("OnPlayerDisconnect(" + peerId + ")");
		matchInfo.GetPlayer(peerId).online = false;
	}
	private void OnReady(bool ready) {
		Debug.Log("OnReady(" + ready + ")");
		if (onReady != null) onReady(ready);
	}
	private void OnPacket(RTPacket packet) {
	}
}
