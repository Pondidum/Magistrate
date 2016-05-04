using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RevokeAddedToUserEvent : UserLoggedEvent
	{
		public RevokeAddedToUserEvent(Operator user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
