using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleAddedToUserEvent : UserLoggedEvent
	{
		public RoleAddedToUserEvent(Operator @operator, Guid roleID) : base(@operator)
		{
			RoleID = roleID;
		}

		public Guid RoleID { get; }
	}
}
