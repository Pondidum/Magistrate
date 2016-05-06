namespace Magistrate.Domain.Events.UserEvents
{
	public class UserDeletedEvent : UserLoggedEvent
	{
		public UserDeletedEvent(Operator @operator, string userName) : base(@operator)
		{
			UserName = userName;
		}

		public string UserName { get; }
		public override string EventDescription => $"Disabled User {UserName}";
	}
}
