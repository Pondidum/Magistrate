namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleNameChangedEvent : UserLoggedEvent
	{
		public RoleNameChangedEvent(MagistrateUser user, string newName) : base(user)
		{
			NewName = newName;
		}

		public string NewName { get; }
	}
}