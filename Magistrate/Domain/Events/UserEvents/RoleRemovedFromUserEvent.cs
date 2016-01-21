using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleRemovedFromUserEvent : UserLoggedEvent
	{
		public RoleRemovedFromUserEvent(MagistrateUser user, Guid roleID) : base(user)
		{
			RoleID = roleID;
		}

		public Guid RoleID { get; }
	}
}
