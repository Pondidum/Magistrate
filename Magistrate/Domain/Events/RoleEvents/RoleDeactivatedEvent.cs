namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDeactivatedEvent : UserLoggedEvent
	{
		public RoleDeactivatedEvent(Operator user, string roleName) : base(user)
		{
			RoleName = roleName;
		}

		public string RoleName { get; }
		public override string EventDescription => $"Disabled Role {RoleName}";
	}
}
