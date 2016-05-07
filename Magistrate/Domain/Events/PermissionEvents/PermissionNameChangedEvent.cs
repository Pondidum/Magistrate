namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionNameChangedEvent : UserLoggedEvent
	{
		public PermissionNameChangedEvent(Operator @operator, string newName) : base(@operator)
		{
			NewName = newName;
		}

		public string NewName { get; }
	}
}
