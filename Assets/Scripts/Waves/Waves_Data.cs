using System.Collections.Generic;
using UnityEngine;

namespace Ingame.waves {
	public class Waves_Data : ILoadableFile {
		public string Name { get; set; }

		private List<Wave> waves = new List<Wave>();
		private int idx = 0;

		public Wave GetNextWave() {
			return idx < waves.Count ? waves[idx++] : null;
		}

		public void Load(string text) {
			string[] lines = text.Split('\n');
			
			int i = 0;
			Wave wave = ReadNextWave(lines, ref i);
			while (wave != null) {
				waves.Add(wave);
				wave = ReadNextWave(lines, ref i);
			}
		}

		private Wave ReadNextWave(string[] lines, ref int i) {
			while (i < lines.Length && string.IsNullOrEmpty(lines[i])) ++i;
			if (i >= lines.Length) return null;

			if (lines[i][0] != '{') {
				Debug.LogError("Error at line " + i + ". Expected {WAVE_NAME}");
				return null;
			}
			++i;
			
			float spawnTime;
			if (!float.TryParse(lines[i], out spawnTime)) {
				Debug.LogError("Error at line " + i + ". Expected spawnTime");
				return null;
			}
			++i;

			Wave wave = new Wave();
			wave.spawnTime = spawnTime;
			
			string[] monsterSplit;
			for (; i < lines.Length && !string.IsNullOrEmpty(lines[i]) && lines[i][0] != '{'; ++i) {
				monsterSplit = lines[i].Split(',');
				if (monsterSplit.Length < 3) {
					Debug.LogError("Error at line " + i + ". Expected MONSTER_NAME, PATH_NAME, SPAWN_TIME");
					return wave;
				}
				
				if (!float.TryParse(monsterSplit[2], out spawnTime)) {
					Debug.LogError("Error at line " + i + ". SPAWN_TIME is not a float");
					return wave;
				}

				wave.monsters.Add(new WaveMonster(monsterSplit[0].Trim(' '), spawnTime, monsterSplit[1].Trim(' ')));
			}

			return wave;
		}
	}
}
