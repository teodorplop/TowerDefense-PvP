using System.IO;

/// <summary>
/// Used to write objects in files.
/// </summary>
public class DataSerializer {
	/// <summary>
	/// Serializes an object into a file at path.
	/// </summary>
	public static void SerializeData<T>(object obj, string path, bool pretty = false) {
		using (TextWriter writer = new StreamWriter(path)) {
			writer.Write(JsonSerializer.Serialize<T>(obj, pretty));
		}
	}
	/// <summary>
	/// Deserializes file at path and returns it as an object of type T.
	/// </summary>
	public static T DeserializeData<T>(string path) {
		using (TextReader reader = new StreamReader(path)) {
			return JsonSerializer.Deserialize<T>(reader.ReadToEnd());
		}
	}
}
