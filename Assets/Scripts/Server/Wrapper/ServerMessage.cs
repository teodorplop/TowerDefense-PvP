namespace Server {
	/// <summary>
	/// Holds information about a message coming from server.
	/// </summary>
	public class ServerMessage {
		private string title;
		public string Title { get { return title; } }
		private string subTitle;
		public string SubTitle { get { return subTitle; } }
		private bool hasErrors;
		public bool HasErrors { get { return hasErrors; } }
		private string jsonString;
		public string JsonString { get { return jsonString; } }
		private ServerObject serverObject;
		public ServerObject ServerObject { get { return serverObject; } }

		public ServerMessage(string title, string subTitle, bool hasErrors, string jsonString, ServerObject serverObject) {
			this.title = title;
			this.subTitle = subTitle;
			this.hasErrors = hasErrors;
			this.jsonString = jsonString;
			this.serverObject = serverObject;
		}

		public override string ToString() {
			return "Has Errors: " + hasErrors + "\n" + "JSON: " + jsonString;
		}
	}
}
