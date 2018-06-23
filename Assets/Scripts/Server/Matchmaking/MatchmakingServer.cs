using System;
using UnityEngine;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Api.Messages;

public class MatchmakingServer {
	private UserProfile profile;
	private Action<MatchInfo> onMatchFound;

	public MatchmakingServer(UserProfile profile) {
		this.profile = profile;

		MatchNotFoundMessage.Listener = OnMatchNotFound;
		MatchFoundMessage.Listener = OnMatchFound;
	}

	private void OnMatchFound(MatchFoundMessage msg) {
		MatchInfo matchInfo = new MatchInfo(msg);

		if (onMatchFound != null) {
			onMatchFound(matchInfo);
			onMatchFound = null;
		}
	}

	private void OnMatchNotFound(MatchNotFoundMessage msg) {
		if (onMatchFound != null) {
			onMatchFound(null);
			onMatchFound = null;
		}
	}

	public void FindMatch(Mod selectedMod, Action<bool> callback, Action<MatchInfo> onMatchFound) {
		this.onMatchFound = onMatchFound;
		new MatchmakingRequest().SetMatchShortCode("rankedMatch").SetSkill(profile.MMR).SetMatchGroup(selectedMod.Hash).Send((response) => FindMatchCallback(response, callback));
	}
	private void FindMatchCallback(MatchmakingResponse response, Action<bool> callback) {
		if (response.HasErrors) {
			Debug.Log(response.Errors.JSON);
			onMatchFound = null;
		}

		if (callback != null)
			callback(!response.HasErrors);
	}

	public void CancelFindMatch(Action<bool> callback) {
		new MatchmakingRequest().SetAction("cancel").SetMatchShortCode("rankedMatch").Send((response) => CancelFindMatchCallback(response, callback));
	}
	private void CancelFindMatchCallback(MatchmakingResponse response, Action<bool> callback) {
		if (response.HasErrors)
			Debug.Log(response.Errors.JSON);
		else
			onMatchFound = null;

		if (callback != null)
			callback(!response.HasErrors);
	}
}
