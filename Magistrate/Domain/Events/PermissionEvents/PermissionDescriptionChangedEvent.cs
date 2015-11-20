namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDescriptionChangedEvent : UserLoggedEvent
	{
		public string NewDescription { get; set; }
	}
}
