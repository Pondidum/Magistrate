namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDescriptionChangedEvent : UserLoggedEvent
	{
		public RoleDescriptionChangedEvent(MagistrateUser user, string newDescription, string roleName) : base(user)
		{
			NewDescription = newDescription;
			RoleName = roleName;
		}

		public string NewDescription { get; }
		public string RoleName { get; }
		public override string EventDescription => $"Changed {RoleName}'s Description to '{NewDescription}'";
	}
}