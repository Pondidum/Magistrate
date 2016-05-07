namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDeletedEvent : UserLoggedEvent
	{
		public RoleDeletedEvent(Operator @operator) : base(@operator)
		{
		}
	}
}
