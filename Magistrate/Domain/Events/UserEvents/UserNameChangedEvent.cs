namespace Magistrate.Domain.Events.UserEvents
{
	public class UserNameChangedEvent : UserLoggedEvent
	{
		public UserNameChangedEvent(MagistrateUser user, string newName) : base(user)
		{
			NewName = newName;
		}

		public string NewName { get; }
	}
}
