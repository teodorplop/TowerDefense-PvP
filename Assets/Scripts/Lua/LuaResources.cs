using UnityEngine;
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
		private static string _resourcesRoot = Path.Combine(Application.streamingAssetsPath, "GameResources");
		private static string _extension = ".lua";
		private static Dictionary<string, LuaWrapper> _resources = new Dictionary<string, LuaWrapper>();

		/// <summary>
		/// Loads all lua data into memory.
		/// </summary>
		public static void LoadAll() {
			UserData.RegisterAssembly();
			IEnumerable<Type> registeredTypes = UserData.GetRegisteredTypes();

			string[] files = DirectoryIO.GetFileNamesRecursively(_resourcesRoot, _extension);
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
			path = Hash(path);

			if (!_resources.ContainsKey(path)) {
				LuaWrapper lua = LoadLua(path, UserData.GetRegisteredTypes());
				if (lua == null) {
					Debug.LogError("No lua at " + path);
				}
				return lua;
			}
			return _resources[path];
		}

		/// <summary>
		/// Loads a lua file at path, also adding type dependencies.
		/// </summary>
		private static LuaWrapper LoadLua(string path, IEnumerable<Type> dependencies) {
			if (!Path.HasExtension(path)) {
				path += _extension;
			}

			LuaWrapper lua = null;
			if (File.Exists(path)) {
				lua = new LuaWrapper(FileIO.GetFileContent(path));
				AddDependencies(lua, dependencies);

				path = path.Remove(0, _resourcesRoot.Length + 1);
				path = path.Remove(path.Length - _extension.Length, _extension.Length);
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
		/// Returns an universal path, with \ instead of /.
		/// </summary>
		private static string Hash(string path) {
			return path.Replace('/', '\\');
		}
	}
}
