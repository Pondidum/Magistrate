namespace Magistrate.Domain.Events
{
	public class RoleDescriptionChangedEvent : UserLoggedEvent
	{
		public string NewDescription { get; set; }
	}
}