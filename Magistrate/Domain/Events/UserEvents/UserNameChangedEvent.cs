namespace Magistrate.Domain.Events.UserEvents
{
	public class UserNameChangedEvent : UserLoggedEvent
	{
		 public string NewName { get; set; }
	}
}
