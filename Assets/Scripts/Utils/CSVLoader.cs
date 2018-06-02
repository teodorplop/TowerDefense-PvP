using System;
using UnityEngine;

/// <summary>
/// First line is skipped!
/// </summary>
public abstract class CSVLoader : IGameResource {
	private string[] lines;
	public string[] Lines { get { return lines; } }

	public string Name { get; set; }

	protected CSVLoader(string text) {
		lines = text.Split('\n');
		Process();
	}

	protected virtual void Process() {
		for (int i = 1; i < lines.Length; i++)
			ProcessLine(i, lines[i].Split(','));
	}

	protected virtual void DebugError(int line, string error) {
		Debug.LogError(GetType() + " error. Line " + line + ": " + error);
	}

	protected virtual void DebugWarning(int line, string error) {
		Debug.LogWarning(GetType() + " error. Line " + line + ": " + error);
	}

	protected abstract void ProcessLine(int line, string[] values);
}