namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDeletedEvent : UserLoggedEvent
	{
		public string PermissionName { get; }

		public PermissionDeletedEvent(Operator @operator, string permissionName) : base(@operator)
		{
			PermissionName = permissionName;
		}

		public override string EventDescription => $"Disabled Permission {PermissionName}";
	}
}
