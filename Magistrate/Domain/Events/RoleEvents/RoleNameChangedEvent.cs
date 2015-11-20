namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleNameChangedEvent : UserLoggedEvent
	{
		public string NewName { get; set; }
	}
}