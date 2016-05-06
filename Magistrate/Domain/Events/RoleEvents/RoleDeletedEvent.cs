namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDeletedEvent : UserLoggedEvent
	{
		public RoleDeletedEvent(Operator @operator, string roleName) : base(@operator)
		{
			RoleName = roleName;
		}

		public string RoleName { get; }
		public override string EventDescription => $"Disabled Role {RoleName}";
	}
}
