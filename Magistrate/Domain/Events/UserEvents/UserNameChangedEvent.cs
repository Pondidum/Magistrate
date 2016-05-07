namespace Magistrate.Domain.Events.UserEvents
{
	public class UserNameChangedEvent : UserLoggedEvent
	{
		public UserNameChangedEvent(Operator @operator, string newName) : base(@operator)
		{
			NewName = newName;
		}

		public string NewName { get; }
	}
}
