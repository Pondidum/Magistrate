using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RevokeAddedToUserEvent : UserLoggedEvent
	{
		public RevokeAddedToUserEvent(Operator @operator, Guid permissionID) : base(@operator)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
