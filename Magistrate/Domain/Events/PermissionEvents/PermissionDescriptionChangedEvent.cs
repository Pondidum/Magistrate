namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionDescriptionChangedEvent : UserLoggedEvent
	{
		public PermissionDescriptionChangedEvent(Operator @operator, string permissionName, string newDescription) : base(@operator)
		{
			PermissionName = permissionName;
			NewDescription = newDescription;
		}

		public string PermissionName { get; }
		public string NewDescription { get; }

		public override string EventDescription => $"Changed {PermissionName}'s Description to '{NewDescription}'";
	}
}
