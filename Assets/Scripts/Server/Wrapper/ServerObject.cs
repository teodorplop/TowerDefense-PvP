using System;
using System.Collections.Generic;

namespace Server {
	/// <summary>
	/// Base class for data received from server.
	/// </summary>
	public abstract class ServerObject {
		public abstract string JSON { get; }
		public abstract bool ContainsKey(string key);
		public abstract IDictionary<string, object> ToDictionary();
		public abstract ServerObject GetServerObject(string key);
		public abstract List<ServerObject> GetServerObjectList(string key);
		public abstract bool? GetBoolean(string key);
		public abstract DateTime? GetDate(string key);
		public abstract double? GetDouble(string key);
		public abstract List<double> GetDoubleList(string key);
		public abstract float? GetFloat(string key);
		public abstract List<float> GetFloatList(string key);
		public abstract int? GetInt(string key);
		public abstract List<int> GetIntList(string key);
		public abstract long? GetLong(string key);
		public abstract List<long> GetLongList(string key);
		public abstract string GetString(string key);
		public abstract List<string> GetStringList(string key);
	}
}
