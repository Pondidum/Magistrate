namespace Magistrate.Domain.Events.UserEvents
{
	public class UserDeactivatedEvent : UserLoggedEvent
	{
		public UserDeactivatedEvent(MagistrateUser user, string userName) : base(user)
		{
			UserName = userName;
		}

		public string UserName { get; }
		public override string EventDescription => $"Disabled User {UserName}";
	}
}
