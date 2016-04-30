namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDeletedEvent : UserLoggedEvent
	{
		public string PermissionName { get; }

		public PermissionDeletedEvent(Operator user, string permissionName) : base(user)
		{
			PermissionName = permissionName;
		}

		public override string EventDescription => $"Disabled Permission {PermissionName}";
	}
}
