namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionNameChangedEvent : UserLoggedEvent
	{
		public PermissionNameChangedEvent(MagistrateUser user, string oldName, string newName) : base(user)
		{
			OldName = oldName;
			NewName = newName;
		}

		public string OldName { get; }
		public string NewName { get; }

		public override string EventDescription => $"Changed {OldName}'s Name to {NewName}";
	}
}
