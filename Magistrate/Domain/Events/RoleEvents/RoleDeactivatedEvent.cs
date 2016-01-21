namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDeactivatedEvent : UserLoggedEvent
	{
		public RoleDeactivatedEvent(MagistrateUser user) : base(user)
		{
		}
	}
}
