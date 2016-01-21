using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RevokeRemovedFromUserEvent : UserLoggedEvent
	{
		public RevokeRemovedFromUserEvent(MagistrateUser user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
