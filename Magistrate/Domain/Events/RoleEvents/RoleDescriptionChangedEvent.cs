namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDescriptionChangedEvent : UserLoggedEvent
	{
		public string NewDescription { get; set; }
	}
}