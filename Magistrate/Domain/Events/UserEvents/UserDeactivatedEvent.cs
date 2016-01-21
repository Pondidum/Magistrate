namespace Magistrate.Domain.Events.UserEvents
{
	public class UserDeactivatedEvent : UserLoggedEvent
	{
		public UserDeactivatedEvent(MagistrateUser user) : base(user)
		{
		}
	}
}
