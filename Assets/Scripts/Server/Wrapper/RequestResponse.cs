namespace Server {
	/// <summary>
	/// Container class for responses coming from server requests.
	/// </summary>
	public class RequestResponse {
		private readonly bool success;
		public bool Success { get { return success; } }
		private readonly string jsonString;
		public string JsonString { get { return jsonString; } }
		private readonly ServerObject serverObject;
		public ServerObject ServerObject { get { return serverObject; } }

		public RequestResponse(bool success, string jsonString, ServerObject serverObject) {
			this.success = success;
			this.jsonString = jsonString;
			this.serverObject = serverObject;
		}

		public override string ToString() {
			return "Success: " + success + "\n" + "JSON: " + jsonString;
		}
	}
}
