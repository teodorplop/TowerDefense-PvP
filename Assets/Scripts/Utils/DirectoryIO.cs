using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Utils.IO {
	public static class DirectoryIO {
		/// <summary>
		/// Returns file names from directory, with a certain extension.
		/// </summary>
		public static string[] GetFileNames(string directory, string extension) {
			if (!Directory.Exists(directory)) {
				Debug.LogError("Directory not found: " + directory);
				return new string[0];
			}

			FileInfo[] filesInfos = new DirectoryInfo(directory).GetFiles();
			List<string> files = new List<string>(filesInfos.Length);
			foreach (FileInfo fileInfo in filesInfos) {
				if (fileInfo.Extension == extension) {
					files.Add(fileInfo.FullName);
				}
			}

			return files.ToArray();
		}

		/// <summary>
		/// Returns file names from directory, with a certain extension. Also includes subdirectories, recursively.
		/// </summary>
		public static string[] GetFileNamesRecursively(string directory, string extension) {
			if (!Directory.Exists(directory)) {
				Debug.LogError("Directory not found: " + directory);
				return new string[0];
			}

			List<string> files = new List<string>();
			GetFileNamesDFS(files, directory, extension);
			return files.ToArray();
		}

		/// <summary>
		/// Adds to a list file names from a root directory, with a certain extension. Also includes subdirectories, recursively.
		/// </summary>
		private static void GetFileNamesDFS(List<string> list, string directory, string extension) {
			list.AddRange(GetFileNames(directory, extension));

			DirectoryInfo[] directoriesInfos = new DirectoryInfo(directory).GetDirectories();
			foreach (DirectoryInfo directoryInfo in directoriesInfos) {
				GetFileNamesDFS(list, directoryInfo.FullName, extension);
			}
		}

		/// <summary>
		/// Returns all subdirectories from directory.
		/// </summary>
		public static string[] GetDirectories(string directory) {
			if (!Directory.Exists(directory)) {
				Debug.LogError("Directory not found: " + directory);
				return new string[0];
			}

			DirectoryInfo[] directoriesInfos = new DirectoryInfo(directory).GetDirectories();
			string[] directories = new string[directoriesInfos.Length];
			for (int i = 0; i < directoriesInfos.Length; ++i) {
				directories[i] = directoriesInfos[i].FullName;
			}
			return directories;
		}
	}
}
