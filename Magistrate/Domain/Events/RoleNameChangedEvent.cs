namespace Magistrate.Domain.Events
{
	public class RoleNameChangedEvent : UserLoggedEvent
	{
		public string NewName { get; set; }
	}
}