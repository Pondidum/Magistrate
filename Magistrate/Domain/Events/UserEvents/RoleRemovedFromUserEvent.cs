using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleRemovedFromUserEvent : UserLoggedEvent
	{
		public RoleRemovedFromUserEvent(Operator @operator, Guid roleID) : base(@operator)
		{
			RoleID = roleID;
		}

		public Guid RoleID { get; }
	}
}
