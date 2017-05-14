using UnityEngine;
using Utils.Linq;

public partial class GameManager {
	private class RequestDispatcher {
		protected ActionHandler[] _handlers = new ActionHandler[0];

		public void SetHandlers(ActionHandler[] handlers) {
			_handlers = handlers;
		}

		public void HandleRequest(ActionRequest request) {
			ActionHandler handler = FindHandlerFor(request);
			if (handler == null) {
				Debug.LogError("No handler available for " + request.GetType() + "\n" + request);
				return;
			}

			handler.Feed(request);
			handler.Execute();
		}

		private ActionHandler FindHandlerFor(ActionRequest request) {
			return _handlers.Find(obj => obj.CanHandle(request));
		}
	}

	public void HandleRequest(ActionRequest request) {
		_dispatcher.HandleRequest(request);
	}
}
