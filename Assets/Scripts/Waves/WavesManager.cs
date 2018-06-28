using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text;

namespace Ingame.waves {
	[Serializable]
	public class Wave {
		/// <summary>
		/// Time from start of the game to this wave spawn
		/// </summary>
		[SerializeField] public float spawnTime;
		[SerializeField] public List<WaveMonster> monsters = new List<WaveMonster>();
		[NonSerialized] public bool finished;

		public Wave() { }

		public Wave(Wave other) {
			spawnTime = other.spawnTime;
			monsters = new List<WaveMonster>();
			foreach (var monster in other.monsters)
				monsters.Add(new WaveMonster(monster));
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Spawn Time " + spawnTime);
			foreach (var m in monsters)
				sb.Append(m);
			sb.AppendLine("Finished " + finished);

			return sb.ToString();
		}
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
		
		public WaveMonster(WaveMonster other) {
			name = other.name;
			spawnTime = other.spawnTime;
			path = other.path;
		}

		public override string ToString() {
			return string.Format("SpawnTime: {0} Name: {1} Path: {2} Sent: {3}\n", spawnTime, name, path, sent);
		}
	}

	public class WavesManager : MonoBehaviour {
		[SerializeField]
		private float _displacement = 2.5f;
		[SerializeField]
		private float _repeatWaveDelay = 15.0f;
		[SerializeField]
		private float _repeatWaveDecay = 1.0f;

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
			if (_currentWave.spawnTime >= _matchTimer && _nextWave == null) {
				Debug.Log("This is the last wave. Cannot queue more monsters.");
				return;
			}

			Wave wave;
			if (!_queuedWaves.TryGetValue(player, out wave)) {
				_queuedWaves.Add(player, wave = new Wave());
				if (_currentWave.spawnTime < _matchTimer)
					wave.spawnTime = _currentWave.spawnTime;
				else
					wave.spawnTime = _nextWave.spawnTime;//_nextWave.spawnTime;
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
			if (_nextWave == null) {
				_nextWave = new Wave(_currentWave);
				_nextWave.spawnTime = _matchTimer + _repeatWaveDelay;
				_repeatWaveDelay = Mathf.Max(2.0f, _repeatWaveDelay - _repeatWaveDecay);
			}

			List<Player> toRemove = new List<Player>();
			foreach (var wave in _queuedWaves)
				if (wave.Value.finished) toRemove.Add(wave.Key);
			foreach (var p in toRemove)
				_queuedWaves.Remove(p);
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
