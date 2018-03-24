using System.Collections.Generic;
using System;
using UnityEngine;

namespace Ingame.waves {
	[Serializable]
	public class Wave {
		/// <summary>
		/// Time from start of the game to this wave spawn
		/// </summary>
		[SerializeField] public float spawnTime;
		[SerializeField] public List<WaveMonster> monsters;
		[NonSerialized] public bool finished;
	}

	[Serializable]
	public class WaveMonster {
		/// <summary>
		/// Time from start of the wave to this minion spawn
		/// </summary>
		[SerializeField] public float spawnTime;
		[SerializeField] public string name;
		[SerializeField] public string path;
		[NonSerialized] public bool sent;

		public WaveMonster(string name, float spawnTime, string path) {
			this.name = name;
			this.spawnTime = spawnTime;
			this.path = path;
		}
	}

	public class WavesManager : MonoBehaviour {
		[SerializeField]
		private float _displacement = 2.5f;

		private Waves_Data _waves;
		private Wave _currentWave, _nextWave;
		private float _matchTimer;
		
		private Dictionary<Player, Wave> _queuedWaves;
		
		private MonsterFactory _monsterFactory;
		void Awake() {
			_waves = GameResources.LoadWaves();
			_queuedWaves = new Dictionary<Player, Wave>();
			_monsterFactory = FindObjectOfType<MonsterFactory>();
		}

		public void StartMatch() {
			_currentWave = _waves.GetNextWave();
			_nextWave = _waves.GetNextWave();
		}
		public void QueueMonster(Player player, string monster, string path) {
			if (_nextWave == null) {
				Debug.Log("This is the last wave. Cannot queue more monsters.");
				return;
			}

			Wave wave;
			if (!_queuedWaves.TryGetValue(player, out wave)) {
				_queuedWaves.Add(player, wave = new Wave());
				wave.spawnTime = _nextWave.spawnTime;
			}
			wave.monsters.Add(new WaveMonster(monster, 0.0f, path));
		}

		void Update() {
			if (_currentWave == null) return;

			_matchTimer += Time.deltaTime;
			
			if (_matchTimer >= _currentWave.spawnTime) {
				ManageWave(_currentWave);
				foreach (var entry in _queuedWaves)
					ManageWave(entry.Value, entry.Key);

				if (_currentWave.finished) ManageNextWave();
			}
		}

		private void ManageWave(Wave wave, Player player = null) {
			if (wave.finished) return;

			float currentWaveTimer = _matchTimer - wave.spawnTime;
			List<WaveMonster> toSend = new List<WaveMonster>();
			wave.finished = true;
			foreach (WaveMonster monster in wave.monsters)
				if (!monster.sent) {
					wave.finished = false;
					if (currentWaveTimer >= monster.spawnTime) {
						toSend.Add(monster);
						monster.sent = true;
					}
				}

			if (player == null) SendMonsters(toSend);
			else SendMonsters(toSend, player);
		}

		private void ManageNextWave() {
			_currentWave = _nextWave;
			_nextWave = _waves.GetNextWave();
			_queuedWaves.Clear();
		}
		
		private void SendMonsters(List<WaveMonster> monsters) {
			foreach (Player player in Players.GetPlayers())
				SendMonsters(monsters, player);
		}

		private void SendMonsters(List<WaveMonster> monsters, Player player) {
			foreach (WaveMonster monster in monsters)
				_monsterFactory.SendMonster(player, monster.name, monster.path, GetRandomOffset());
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
