namespace Magistrate.Domain.Events
{
	public class PermissionDescriptionChangedEvent : UserLoggedEvent
	{
		public string NewDescription { get; set; }
	}
}
