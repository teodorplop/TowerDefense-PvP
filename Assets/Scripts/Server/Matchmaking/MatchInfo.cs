using System.Text;
using System.Collections.Generic;
using GameSparks.Api.Messages;

public class PlayerInfo {
	private string displayName;
	private int mmr;
	private string id;
	private int peerId;
	
	public bool clientPlayer = false;
	public bool online = true;

	public string DisplayName { get { return displayName; } }
	public int MMR { get { return mmr; } }
	public string Id { get { return id; } }
	public int PeerId { get { return peerId; } }

	public PlayerInfo(string displayName, string id, int peerId, int mmr) {
		this.displayName = displayName;
		this.id = id;
		this.peerId = peerId;
		this.mmr = mmr;
	}
}

public class MatchInfo {
	private string map = "Map0";

	private string hostURL;
	private int portId;
	private string accessToken;
	private string matchId;
	private List<PlayerInfo> players;

	public string Map { get { return map; } }
	public string HostURL { get { return hostURL; } }
	public int PortId { get { return portId; } }
	public string AccessToken { get { return accessToken; } }
	public string MatchId { get { return matchId; } }
	public List<PlayerInfo> Players { get { return players; } }

	public MatchInfo() {
		players = new List<PlayerInfo>() {
			new PlayerInfo("Manthril", "0", 0, 1000),
			new PlayerInfo("Hetamess", "1", 1, 1000)
		};
		players[0].clientPlayer = true;
	}

	public MatchInfo(MatchFoundMessage msg) {
		portId = (int)msg.Port;
		hostURL = msg.Host;
		accessToken = msg.AccessToken;
		matchId = msg.MatchId;

		players = new List<PlayerInfo>();
		foreach (var p in msg.Participants) {
			players.Add(new PlayerInfo(p.DisplayName, p.Id, (int)p.PeerId, (int)p.ScriptData.GetGSData("stats").GetInt("mmr")));
		}
	}

	public PlayerInfo GetClientPlayer() {
		return players.Find(obj => obj.clientPlayer);
	}

	public PlayerInfo GetPlayer(int peerId) {
		return players.Find(obj => obj.PeerId == peerId);
	}

	public override string ToString() {
		StringBuilder sb = new StringBuilder("MatchInfo\n");
		sb.AppendFormat("HostURL {0}\nPortId {1}\nAccessToken {2}\nMatchId {3}\nPlayers\n", hostURL, portId, accessToken, matchId);
		foreach (PlayerInfo p in players)
			sb.AppendFormat("DisplayName {0} Id {1} PeerId {2} Online {3}\n", p.DisplayName, p.Id, p.PeerId, p.online);
		return sb.ToString();
	}
}
