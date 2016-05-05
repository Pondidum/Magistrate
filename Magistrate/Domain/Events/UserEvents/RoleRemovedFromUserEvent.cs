using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleRemovedFromUserEvent : UserLoggedEvent
	{
		public RoleRemovedFromUserEvent(Operator user, Guid roleID) : base(user)
		{
			RoleID = roleID;
		}

		public Guid RoleID { get; }
	}
}
