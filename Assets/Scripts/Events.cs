#region Utils
public class OneElementEvent<T> : GameEvent {
	private T t;
	public T Element { get { return t; } }
	public OneElementEvent(T t) {
		this.t = t;
	}
}
public class TwoElementsEvent<T, U> : GameEvent {
	private T t;
	private U u;

	public T First { get { return t; } }
	public U Second { get { return u; } }

	public TwoElementsEvent(T t, U u) {
		this.t = t;
		this.u = u;
	}
}
#endregion

#region Login
public class ServerRequestEvent : OneElementEvent<string> {
	public ServerRequestEvent(string msg) : base(msg) { }
}
public class ServerResponseEvent : OneElementEvent<string> {
	public ServerResponseEvent(string msg) : base(msg) { }
}

public class LoggedInEvent : OneElementEvent<UserProfile> {
	public LoggedInEvent(UserProfile profile) : base(profile) { }
}
public class LoggedOutEvent : GameEvent { }
#endregion