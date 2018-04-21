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

#region Multiplayer
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

public class FindMatchStartedEvent : GameEvent { }
public class FindMatchCanceledEvent : GameEvent { }
public class MatchFoundEvent : GameEvent { }
#endregion

#region Ingame
public class UpgradeTowerEvent : OneElementEvent<UpgradeTowerRequest> {
	public UpgradeTowerEvent(UpgradeTowerRequest towerRequest) : base(towerRequest) { }
}
public class SellTowerEvent : OneElementEvent<SellTowerRequest> {
	public SellTowerEvent(SellTowerRequest towerRequest) : base(towerRequest) { }
}
public class SendMonsterEvent : OneElementEvent<SendMonsterRequest> {
	public SendMonsterEvent(SendMonsterRequest sendRequest) : base(sendRequest) { }
}
public class MatchOverEvent : OneElementEvent<string> {
	public MatchOverEvent(string winnerId) : base(winnerId) { }
}
#endregion