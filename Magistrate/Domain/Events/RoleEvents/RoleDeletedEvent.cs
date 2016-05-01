namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDeletedEvent : UserLoggedEvent
	{
		public RoleDeletedEvent(Operator user, string roleName) : base(user)
		{
			RoleName = roleName;
		}

		public string RoleName { get; }
		public override string EventDescription => $"Disabled Role {RoleName}";
	}
}
