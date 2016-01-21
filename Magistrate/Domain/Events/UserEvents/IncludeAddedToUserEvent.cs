using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeAddedToUserEvent : UserLoggedEvent
	{
		public IncludeAddedToUserEvent(MagistrateUser user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
