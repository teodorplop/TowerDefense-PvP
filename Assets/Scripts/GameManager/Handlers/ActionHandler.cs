public partial class GameManager {
	private class ActionHandler {
		protected ActionRequest _request = null;

		public virtual bool CanHandle(ActionRequest request) {
			return false;
		}

		public virtual void Feed(ActionRequest request) {
			_request = request;
		}

		public virtual void Execute() {}
	}
}
