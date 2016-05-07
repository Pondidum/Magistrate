namespace Magistrate.Domain.Events.UserEvents
{
	public class UserDeletedEvent : UserLoggedEvent
	{
		public UserDeletedEvent(Operator @operator) : base(@operator)
		{
		}
	}
}
