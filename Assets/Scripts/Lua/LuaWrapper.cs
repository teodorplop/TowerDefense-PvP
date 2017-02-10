using UnityEngine;
using MoonSharp.Interpreter;

namespace Lua {
	/// <summary>
	/// Class used to call lua functions inside C#
	/// </summary>
	public class LuaWrapper {
		private Script _script;
		public LuaWrapper(string source) {
			_script = new Script();
			_script.DoString(source);
		}

		public void SetGlobal(string name, object obj) {
			_script.Globals[name] = obj;
		}
		public object Call(string function, params object[] args) {
			DynValue value = null;
			try {
				value = _script.Call(_script.Globals.Get(function), args);
			} catch (ScriptRuntimeException e) {
				Debug.LogError("Error occured!\n" + e.DecoratedMessage);
			}
			return value != null ? value.ToObject() : null;
		}
	}
}
