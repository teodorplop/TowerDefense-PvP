using System.Collections.Generic;
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
		public string path;

		[NonSerialized]
		public bool sent;
	}

	public class WavesManager : MonoBehaviour {
		private struct MonsterToSend {
			public string name;
			public string path;
			public MonsterToSend(string name, string path) {
				this.name = name;
				this.path = path;
			}
		}

		[SerializeField]
		private float _displacement = 2.5f;

		private List<Wave> _waves;
		private Dictionary<Player, List<MonsterToSend>> _queuedMonsters;

		private bool _started;
		private float _matchTimer;
		private MonsterFactory _monsterFactory;
		void Awake() {
			_waves = GameResources.LoadWaves();
			_queuedMonsters = new Dictionary<Player, List<MonsterToSend>>();
			_monsterFactory = FindObjectOfType<MonsterFactory>();
		}

		public void StartMatch() {
			_started = true;
		}
		public void QueueMonster(Player player, string monster, string path) {
			if (!_queuedMonsters.ContainsKey(player))
				_queuedMonsters.Add(player, new List<MonsterToSend>());
			_queuedMonsters[player].Add(new MonsterToSend(monster, path));
		}

		void Update() {
			if (!_started) return;

			_matchTimer += Time.deltaTime;

			foreach (Wave wave in _waves)
				if (_matchTimer >= wave.spawnTime && !wave.finished)
					SendMonsters(wave.Advance(Time.deltaTime));

			foreach (Player player in _queuedMonsters.Keys) {
				foreach (MonsterToSend monster in _queuedMonsters[player])
					_monsterFactory.SendMonster(player, monster.name, monster.path, GetRandomOffset());
				_queuedMonsters[player].Clear();
			}
		}

		private void SendMonsters(List<WaveMonster> monsters) {
			foreach (WaveMonster monster in monsters) {
				foreach (Player player in Players.GetPlayers())
					_monsterFactory.SendMonster(player, monster.name, monster.path, GetRandomOffset());
			}
		}

		private Vector2 GetRandomOffset() {
			System.Random random = new System.Random(0);
			Vector2 offset = Vector2.zero;
			offset.x = ((float)random.NextDouble() * 2 - 1) * _displacement;
			offset.y = ((float)random.NextDouble() * 2 - 1) * _displacement;
			return offset;
		}
	}
}
