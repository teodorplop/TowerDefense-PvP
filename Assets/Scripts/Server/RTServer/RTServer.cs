using UnityEngine;
using GameSparks.RT;
using System;

public class RTServer {
	private const int UPGRADETOWER_OPCODE = 120;
	private const int SELLTOWER_OPCODE = 121;

	private MatchInfo matchInfo;
	private Action<bool> onReady;

	private int[] otherPeerIds;
	private int PeerId { get { return (int)GameSparksRTUnity.Instance.PeerId; } }

	public RTServer(MatchInfo matchInfo) {
		this.matchInfo = matchInfo;

		EventManager.AddListener<UpgradeTowerEvent>(OnTowerUpgraded);
		EventManager.AddListener<SellTowerEvent>(OnTowerSold);
	}

	public void Connect(Action<bool> onReady) {
		this.onReady = onReady;
		
		GameSparksRTUnity.Instance.Configure(matchInfo.HostURL, matchInfo.PortId, matchInfo.AccessToken, 
			OnPlayerConnect, OnPlayerDisconnect, OnReady, OnPacket);
		GameSparksRTUnity.Instance.Connect();
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

		if (ready) {
			otherPeerIds = new int[matchInfo.Players.Count - 1];
			int idx = 0;
			foreach (PlayerInfo pi in matchInfo.Players)
				if (pi.PeerId == PeerId) pi.clientPlayer = true;
				else otherPeerIds[idx++] = pi.PeerId;
		}

		if (onReady != null) {
			onReady(ready);
			onReady = null;
		}
	}
	private void OnPacket(RTPacket packet) {
		ActionRequest req = null;
		if (packet.OpCode == UPGRADETOWER_OPCODE)
			req = new UpgradeTowerRequest(packet.Data.GetString(1), packet.Data.GetString(2), packet.Data.GetString(3));
		else if (packet.OpCode == SELLTOWER_OPCODE)
			req = new SellTowerRequest(packet.Data.GetString(1), packet.Data.GetString(2));

		if (req != null)
			GameManager.Instance.HandleRequest(req);
	}

	private void OnTowerUpgraded(UpgradeTowerEvent evt) {
		if (matchInfo.GetClientPlayer().Id != evt.Element.Player) return;

		using (RTData data = RTData.Get()) {
			data.SetString(1, evt.Element.Player);
			data.SetString(2, evt.Element.Tower);
			data.SetString(3, evt.Element.Upgrade);
			GameSparksRTUnity.Instance.SendData(UPGRADETOWER_OPCODE, GameSparksRT.DeliveryIntent.RELIABLE, data, otherPeerIds);
		}
	}
	private void OnTowerSold(SellTowerEvent evt) {
		if (matchInfo.GetClientPlayer().Id != evt.Element.Player) return;

		using (RTData data = RTData.Get()) {
			data.SetString(1, evt.Element.Player);
			data.SetString(2, evt.Element.Tower);

			GameSparksRTUnity.Instance.SendData(SELLTOWER_OPCODE, GameSparksRT.DeliveryIntent.RELIABLE, data, otherPeerIds);
		}
	}
}
