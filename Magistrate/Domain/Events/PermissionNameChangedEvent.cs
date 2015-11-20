namespace Magistrate.Domain.Events
{
	public class PermissionNameChangedEvent : UserLoggedEvent
	{
		public string NewName { get; set; }
	}
}