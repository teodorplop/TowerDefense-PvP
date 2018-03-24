using System;
using FullSerializer;

public class JsonSerializer {
	private static readonly fsSerializer _serializer = new fsSerializer();

	/// <summary>
	/// Serializes an object of template type T.
	/// </summary>
	public static string Serialize<T>(object value, bool pretty = false) {
		// serialize the data
		fsData data;
		_serializer.TrySerialize(typeof(T), value, out data).AssertSuccessWithoutWarnings();

		// emit the data via JSON
		return pretty ? fsJsonPrinter.PrettyJson(data) : fsJsonPrinter.CompressedJson(data);
	}
	/// <summary>
	/// Deserializes a string and returns it as an object of template type T.
	/// </summary>
	public static T Deserialize<T>(string serializedState) {
		// step 1: parse the JSON data
		fsData data = fsJsonParser.Parse(serializedState);
		
		// step 2: deserialize the data
		object deserialized = null;
		_serializer.TryDeserialize(data, typeof(T), ref deserialized).AssertSuccessWithoutWarnings();
		
		return (T)deserialized;
	}
}
