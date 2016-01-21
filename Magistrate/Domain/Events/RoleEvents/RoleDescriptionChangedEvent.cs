namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDescriptionChangedEvent : UserLoggedEvent
	{
		public RoleDescriptionChangedEvent(MagistrateUser user, string newDescription) : base(user)
		{
			NewDescription = newDescription;
		}

		public string NewDescription { get; }
	}
}