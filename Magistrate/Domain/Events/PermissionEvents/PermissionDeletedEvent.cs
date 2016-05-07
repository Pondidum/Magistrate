namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDeletedEvent : UserLoggedEvent
	{
		public PermissionDeletedEvent(Operator @operator) : base(@operator)
		{
		}
	}
}
