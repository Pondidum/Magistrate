namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDescriptionChangedEvent : UserLoggedEvent
	{
		public PermissionDescriptionChangedEvent(Operator @operator, string newDescription) : base(@operator)
		{
			NewDescription = newDescription;
		}

		public string NewDescription { get; }
	}
}
