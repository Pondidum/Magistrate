namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleNameChangedEvent : UserLoggedEvent
	{
		public RoleNameChangedEvent(Operator user, string oldName, string newName) : base(user)
		{
			NewName = newName;
			OldName = oldName;
		}

		public string NewName { get; }
		public string OldName { get; }
		public override string EventDescription => $"Changed {OldName}'s Name to {NewName}";
	}
}
