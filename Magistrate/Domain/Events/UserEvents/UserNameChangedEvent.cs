namespace Magistrate.Domain.Events.UserEvents
{
	public class UserNameChangedEvent : UserLoggedEvent
	{
		public UserNameChangedEvent(Operator @operator, string oldName, string newName) : base(@operator)
		{
			NewName = newName;
			OldName = oldName;
		}

		public string NewName { get; }
		public string OldName { get; }
		public override string EventDescription => $"Changed {OldName}'s Name to {NewName}";
	}
}
