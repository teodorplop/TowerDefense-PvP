using System.IO;

namespace Utils.IO {
	public static class FileIO {
		/// <summary>
		/// Adds a content string to a file. If file does not exist, it will be created.
		/// </summary>
		public static void AddContent(string filePath, string content) {
			WriteContent(filePath, GetFileContent(filePath) + content);
		}
		/// <summary>
		/// Writes content string to a file. If file does not exist, it will be created.
		/// </summary>
		public static void WriteContent(string filePath, string content) {
			CreateFile(filePath);

			using (StreamWriter writer = new StreamWriter(filePath)) {
				writer.Write(content);
			}
		}
		/// <summary>
		/// Clear all content from a file. If file does not exist, it will be created.
		/// </summary>
		public static void ClearFileContent(string filePath) {
			CreateFile(filePath);

			using (FileStream stream = new FileStream(filePath, FileMode.Truncate)) { }
		}
		/// <summary>
		/// Returns the content of the file, with the option to create it if it does not exist.
		/// </summary>
		public static string GetFileContent(string filePath, bool createFile = true) {
			if (createFile) {
				CreateFile(filePath);
			} else if (!File.Exists(filePath)) {
				return "";
			}

			string fileContent = "";
			using (StreamReader reader = new StreamReader(filePath)) {
				fileContent = reader.ReadToEnd();
			}
			return fileContent;
		}
		/// <summary>
		/// Creates a file at a given path.
		/// </summary>
		public static void CreateFile(string filePath) {
			string directory = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directory)) {
				Directory.CreateDirectory(directory);
			}
			if (!File.Exists(filePath)) {
				File.Create(filePath).Close();
			}
		}
		/// <summary>
		/// Deletes file at path.
		/// </summary>
		public static void DeleteFile(string filePath) {
			if (File.Exists(filePath)) {
				File.Delete(filePath);
			}
		}
	}
}
