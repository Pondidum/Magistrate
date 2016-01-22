namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDescriptionChangedEvent : UserLoggedEvent
	{
		public PermissionDescriptionChangedEvent(MagistrateUser user, string permissionName, string newDescription) : base(user)
		{
			PermissionName = permissionName;
			NewDescription = newDescription;
		}

		public string PermissionName { get; }
		public string NewDescription { get; }

		public override string EventDescription => $"Changed {PermissionName}'s Description to '{NewDescription}'";
	}
}
