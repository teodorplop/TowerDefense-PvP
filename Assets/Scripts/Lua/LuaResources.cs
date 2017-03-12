﻿using UnityEngine;
using System;
using System.IO;
using Utils.IO;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace Lua {
	/// <summary>
	/// Loads and unloads lua objects.
	/// </summary>
	public static class LuaResources {
		private static string resourcesRoot { get { return Path.Combine(Application.streamingAssetsPath, "GameResources").Replace('/', '\\'); } }

		private static string _extension = ".lua";
		private static Dictionary<string, LuaWrapper> _resources = new Dictionary<string, LuaWrapper>();

		/// <summary>
		/// Loads all lua data into memory.
		/// </summary>
		public static void LoadAll() {
			UserData.RegisterAssembly();
			IEnumerable<Type> registeredTypes = UserData.GetRegisteredTypes();

			string[] files = DirectoryIO.GetFileNamesRecursively(resourcesRoot, _extension);
			foreach (string file in files) {
				LoadLua(file, registeredTypes);
			}
		}

		/// <summary>
		/// Unloads all lua data from memory.
		/// </summary>
		public static void UnloadAll() {
			_resources.Clear();
		}

		/// <summary>
		/// Returns lua loaded at path.
		/// </summary>
		public static LuaWrapper Load(string path) {
			string hash = Hash(path);

			if (!_resources.ContainsKey(hash)) {
				LuaWrapper lua = LoadLua(path, UserData.GetRegisteredTypes());
				if (lua == null) {
					Debug.LogError("No lua at " + path);
				}
				return lua;
			}
			return _resources[hash];
		}

		/// <summary>
		/// Loads a lua file at path, also adding type dependencies.
		/// </summary>
		private static LuaWrapper LoadLua(string path, IEnumerable<Type> dependencies) {
			path = FullPath(path);

			LuaWrapper lua = null;
			if (File.Exists(path)) {
				lua = new LuaWrapper(FileIO.GetFileContent(path));
				AddDependencies(lua, dependencies);
				_resources.Add(Hash(path), lua);
			} else {
				Debug.LogError("No file found at " + path);
			}

			return lua;
		}

		/// <summary>
		/// Add type dependencies to a lua wrapper.
		/// </summary>
		private static void AddDependencies(LuaWrapper lua, IEnumerable<Type> dependencies) {
			if (dependencies != null) {
				foreach (Type type in dependencies) {
					lua.SetGlobal(type.Name, type);
				}
			}
		}

		/// <summary>
		/// Compares two paths
		/// </summary>
		private static bool PathsAreEqual(string path1, string path2) {
			return path1.Replace('/', '\\') == path2.Replace('/', '\\');
		}

		/// <summary>
		/// Returns full given path.
		/// </summary>
		private static string FullPath(string path) {
			if (path.Length < resourcesRoot.Length || !PathsAreEqual(path.Substring(0, resourcesRoot.Length), resourcesRoot)) {
				path = Path.Combine(resourcesRoot, path);
			}
			if (!Path.HasExtension(path)) {
				path += _extension;
			}
			return path;
		}

		/// <summary>
		/// Returns hash equal with relative path.
		/// </summary>
		private static string Hash(string path) {
			path = path.Replace('/', '\\');
			if (path.Length >= resourcesRoot.Length && PathsAreEqual(path.Substring(0, resourcesRoot.Length), resourcesRoot)) {
				path = path.Remove(0, resourcesRoot.Length + 1);
			}
			if (path.Length >= _extension.Length &&
				path.Substring(path.Length - _extension.Length, _extension.Length) == _extension) {
				path = path.Remove(path.Length - _extension.Length, _extension.Length);
			}

			return path;
		}
	}
}
