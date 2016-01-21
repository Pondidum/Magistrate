using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleAddedToUserEvent : UserLoggedEvent
	{
		public RoleAddedToUserEvent(MagistrateUser user, Guid roleID) : base(user)
		{
			RoleID = roleID;
		}

		public Guid RoleID { get; }
	}
}
