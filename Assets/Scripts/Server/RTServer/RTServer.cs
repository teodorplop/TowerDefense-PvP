using UnityEngine;
using System.Collections.Generic;
using GameSparks;
using GameSparks.Core;
using GameSparks.Api.Responses;
using GameSparks.RT;

public class RTServer {
	private MatchInfo matchInfo;

	public RTServer(MatchInfo matchInfo) {
		this.matchInfo = matchInfo;
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

	private void OnPlayerConnect(int peerId) {
		Debug.Log("OnPlayerConnect(" + peerId + ")");
	}
	private void OnPlayerDisconnect(int peerId) {
		Debug.Log("OnPlayerDisconnect(" + peerId + ")");
	}
	private void OnReady(bool ready) {
		Debug.Log("OnReady(" + ready + ")");
	}
	private void OnPacket(RTPacket packet) {
	}
}
