namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionNameChangedEvent : UserLoggedEvent
	{
		public string NewName { get; set; }
	}
}