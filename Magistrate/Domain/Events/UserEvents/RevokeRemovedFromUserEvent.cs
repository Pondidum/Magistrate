using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RevokeRemovedFromUserEvent : UserLoggedEvent
	{
		public RevokeRemovedFromUserEvent(Operator @operator, Guid permissionID) : base(@operator)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
