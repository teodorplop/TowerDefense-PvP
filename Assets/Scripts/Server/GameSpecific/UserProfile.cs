using UnityEngine;
using System;

public class UserProfile {
	private struct Stats {
		public int mmr;
		public int gamesWon;
		public int gamesPlayed;
	}

	[SerializeField] private string displayName;
	[SerializeField] private Stats stats;

	public string DisplayName { get { return displayName; } }
	public int MMR { get { return stats.mmr; } }
	public int GamesWon { get { return stats.gamesWon; } }
	public int GamesPlayed { get { return stats.gamesPlayed; } }
}
