using System;
using System.Collections.Generic;
using GameSparks.Core;

namespace Server {

	/// <summary>
	/// GameSparks data received from server.
	/// </summary>
	public class GameSparksServerObject : ServerObject {
		private GSData data;
		public GameSparksServerObject(GSData data) {
			this.data = data;
		}

		public override string JSON { get { return data.JSON; } }
		public override bool ContainsKey(string key) {
			return data.ContainsKey(key);
		}
		public override IDictionary<string, object> ToDictionary() {
			return data.BaseData;
		}
		public override ServerObject GetServerObject(string key) {
			return new GameSparksServerObject(data.GetGSData(key));
		}
		public override List<ServerObject> GetServerObjectList(string key) {
			List<GSData> gsDataList = data.GetGSDataList(key);
			List<ServerObject> list = new List<ServerObject>(gsDataList.Count);
			foreach (GSData gsData in gsDataList) {
				list.Add(new GameSparksServerObject(gsData));
			}
			return list;
		}
		public override bool? GetBoolean(string key) {
			return data.GetBoolean(key);
		}
		public override DateTime? GetDate(string key) {
			return data.GetDate(key);
		}
		public override double? GetDouble(string key) {
			return data.GetDouble(key);
		}
		public override List<double> GetDoubleList(string key) {
			return data.GetDoubleList(key);
		}
		public override float? GetFloat(string key) {
			return data.GetFloat(key);
		}
		public override List<float> GetFloatList(string key) {
			return data.GetFloatList(key);
		}
		public override int? GetInt(string key) {
			return data.GetInt(key);
		}
		public override List<int> GetIntList(string key) {
			return data.GetIntList(key);
		}
		public override long? GetLong(string key) {
			return data.GetLong(key);
		}
		public override List<long> GetLongList(string key) {
			return data.GetLongList(key);
		}
		public override string GetString(string key) {
			return data.GetString(key);
		}
		public override List<string> GetStringList(string key) {
			return data.GetStringList(key);
		}
	}
}
