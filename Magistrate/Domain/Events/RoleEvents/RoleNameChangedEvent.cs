namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleNameChangedEvent : UserLoggedEvent
	{
		public RoleNameChangedEvent(Operator @operator, string newName) : base(@operator)
		{
			NewName = newName;
		}

		public string NewName { get; }
	}
}
