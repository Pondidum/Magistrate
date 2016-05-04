namespace Magistrate.Domain.Events.UserEvents
{
	public class UserDeletedEvent : UserLoggedEvent
	{
		public UserDeletedEvent(Operator user, string userName) : base(user)
		{
			UserName = userName;
		}

		public string UserName { get; }
		public override string EventDescription => $"Disabled User {UserName}";
	}
}
