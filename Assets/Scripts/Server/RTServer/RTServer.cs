using UnityEngine;
using GameSparks.RT;
using System;
using System.Collections;

public class RTServer {
	private const int PLAYERSJOINED_OPCODE = 100;
	private const int LATENCY_OPCODE = 101;

	private const int UPGRADETOWER_OPCODE = 120;
	private const int SELLTOWER_OPCODE = 121;
	private const int SENDMONSTER_OPCODE = 122;

	private const int GAMEOVER_OPCODE = 130;

	private GameServer gameServer;
	private MatchInfo matchInfo;
	private Action<bool> onReady;

	private int[] otherPeerIds;

	private int roundTrip, latency, timeDelta;

	private int PeerId { get { return (int)GameSparksRTUnity.Instance.PeerId; } }

	public int RoundTrip { get { return roundTrip; } }
	public int Latency { get { return latency; } }

	public RTServer(GameServer gameServer, MatchInfo matchInfo) {
		this.matchInfo = matchInfo;

		EventManager.AddListener<UpgradeTowerEvent>(OnTowerUpgraded);
		EventManager.AddListener<SellTowerEvent>(OnTowerSold);
		EventManager.AddListener<SendMonsterEvent>(OnMonsterSent);
		EventManager.AddListener<MatchOverEvent>(OnMatchOver);
	}

	public void Connect(Action<bool> onReady) {
		if (matchInfo.IsFake) {
			MacroSystem.SetMacroValue("LATENCY_VALUE", latency = 26);
			if (onReady != null) onReady(true);
			return;
		}

		this.onReady = onReady;
		
		GameSparksRTUnity.Instance.Configure(matchInfo.HostURL, matchInfo.PortId, matchInfo.AccessToken, 
			OnPlayerConnect, OnPlayerDisconnect, OnReady, OnPacket);
		GameSparksRTUnity.Instance.Connect();
	}

	private IEnumerator CheckLatency() {
		while (true) {
			using (RTData data = RTData.Get()) {
				data.SetLong(1, (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);
				GameSparksRTUnity.Instance.SendData(LATENCY_OPCODE, GameSparksRT.DeliveryIntent.UNRELIABLE, data, new int[] { 0 });
			}

			yield return new WaitForSeconds(5.0f);
		}
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
	}
	private void OnPacket(RTPacket packet) {
		if (packet.OpCode == PLAYERSJOINED_OPCODE) {
			otherPeerIds = new int[matchInfo.Players.Count - 1];
			int idx = 0;
			foreach (PlayerInfo pi in matchInfo.Players)
				if (pi.PeerId == PeerId) pi.clientPlayer = true;
				else otherPeerIds[idx++] = pi.PeerId;

			if (onReady != null) {
				onReady(true);
				onReady = null;
			}	
			
			CoroutineStarter.CoroutineStart(CheckLatency());

			return;
		}

		if (packet.OpCode == LATENCY_OPCODE) {
			roundTrip = (int)((long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds - packet.Data.GetLong(1).Value);
			latency = roundTrip / 2;
			int serverDelta = (int)(packet.Data.GetLong(2).Value - (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);
			timeDelta = serverDelta + latency;

			MacroSystem.SetMacroValue("LATENCY_VALUE", latency);

			return;
		}

		ActionRequest req = null;
		if (packet.OpCode == UPGRADETOWER_OPCODE)
			req = new UpgradeTowerRequest(packet.Data.GetString(1), packet.Data.GetString(2), packet.Data.GetString(3));
		else if (packet.OpCode == SELLTOWER_OPCODE)
			req = new SellTowerRequest(packet.Data.GetString(1), packet.Data.GetString(2));
		else if (packet.OpCode == SENDMONSTER_OPCODE)
			req = new SendMonsterRequest(packet.Data.GetString(1), new MonsterToSend(packet.Data.GetString(2), (int)packet.Data.GetInt(3)));

		if (req != null)
			GameManager.Instance.HandleRequest(req);
	}

	private void OnTowerUpgraded(UpgradeTowerEvent evt) {
		if (matchInfo.IsFake) return;
		if (matchInfo.GetClientPlayer().Id != evt.Element.Player) return;

		using (RTData data = RTData.Get()) {
			data.SetString(1, evt.Element.Player);
			data.SetString(2, evt.Element.Tower);
			data.SetString(3, evt.Element.Upgrade);

			GameSparksRTUnity.Instance.SendData(UPGRADETOWER_OPCODE, GameSparksRT.DeliveryIntent.RELIABLE, data, otherPeerIds);
		}
	}
	private void OnTowerSold(SellTowerEvent evt) {
		if (matchInfo.IsFake) return;
		if (matchInfo.GetClientPlayer().Id != evt.Element.Player) return;

		using (RTData data = RTData.Get()) {
			data.SetString(1, evt.Element.Player);
			data.SetString(2, evt.Element.Tower);

			GameSparksRTUnity.Instance.SendData(SELLTOWER_OPCODE, GameSparksRT.DeliveryIntent.RELIABLE, data, otherPeerIds);
		}
	}
	private void OnMonsterSent(SendMonsterEvent evt) {
		if (matchInfo.IsFake) return;
		if (matchInfo.GetClientPlayer().Id != evt.Element.PlayerOwner) return;

		using (RTData data = RTData.Get()) {
			data.SetString(1, evt.Element.PlayerOwner);
			data.SetString(2, evt.Element.Monster.name);
			data.SetInt(3, evt.Element.Monster.cost);

			GameSparksRTUnity.Instance.SendData(SENDMONSTER_OPCODE, GameSparksRT.DeliveryIntent.RELIABLE, data, otherPeerIds);
		}
	}
	private void OnMatchOver(MatchOverEvent evt) {
		if (matchInfo.IsFake) {
			OnFakeMatchOver(evt);
			return;
		}

		using (RTData data = RTData.Get()) {
			data.SetString(1, evt.Element);

			GameSparksRTUnity.Instance.SendData(GAMEOVER_OPCODE, GameSparksRT.DeliveryIntent.RELIABLE, data, new int[] { 0 });
		}
	}

	private void OnFakeMatchOver(MatchOverEvent evt) {
		if (evt.Element == Players.ClientPlayer.Id) {
			gameServer.AddMMR(5);
		} else {
			gameServer.AddMMR(-5);
		}
	}
}
