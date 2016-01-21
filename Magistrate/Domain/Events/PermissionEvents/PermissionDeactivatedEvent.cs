namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDeactivatedEvent : UserLoggedEvent
	{
		public string PermissionName { get; }

		public PermissionDeactivatedEvent(MagistrateUser user, string permissionName) : base(user)
		{
			PermissionName = permissionName;
		}

		public override string EventDescription
		{
			get { return $"Disabled {PermissionName}"; }
		}
	}
}
