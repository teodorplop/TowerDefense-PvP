﻿using System.Collections.Generic;
using System;
using UnityEngine;

namespace Ingame.waves {
	[Serializable]
	public class Wave {
		[SerializeField, Tooltip("Time from start of match until this wave spawn")]
		public float spawnTime;
		[SerializeField]
		public List<WaveMonster> monsters;

		[NonSerialized]
		private float _timer;
		[NonSerialized]
		public bool finished;

		public List<WaveMonster> Advance(float time) {
			_timer += time;

			finished = true;
			List<WaveMonster> monstersToSend = new List<WaveMonster>();
			foreach (WaveMonster monster in monsters) {
				if (!monster.sent) {
					finished = false;
					if (_timer >= monster.spawnTime) {
						monster.sent = true;
						monstersToSend.Add(monster);
					}
				}
			}

			return monstersToSend;
		}
	}

	[Serializable]
	public class WaveMonster {
		[SerializeField, Tooltip("Time from start of wave until these monsters spawn")]
		public float spawnTime;
		[SerializeField]
		public string name;
		[SerializeField]
		public int count;
		[SerializeField]
		public string path;

		[NonSerialized]
		public bool sent;
	}

	public class WavesManager : MonoBehaviour {
		[SerializeField]
		private List<Wave> _waves;

		private float _matchTimer;
		private MonsterFactory _monsterFactory;
		private List<Player> _players;
		void Awake() {
			_monsterFactory = FindObjectOfType<MonsterFactory>();
			_players = new List<Player>();
		}

		public void Register(Player player) {
			_players.Add(player);
		}

		void FixedUpdate() {
			_matchTimer += Time.fixedDeltaTime;

			foreach (Wave wave in _waves) {
				if (_matchTimer >= wave.spawnTime && !wave.finished) {
					SendMonsters(wave.Advance(Time.fixedDeltaTime));
				}
			}
		}

		private void SendMonsters(List<WaveMonster> monsters) {
			foreach (WaveMonster monster in monsters) {
				for (int i = 0; i < _players.Count; ++i) {
					_monsterFactory.SendMonster(_players[i], monster.name, monster.count, monster.path);
				}
			}
		}
	}
}