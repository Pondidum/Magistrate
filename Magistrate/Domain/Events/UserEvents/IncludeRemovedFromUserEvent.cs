using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeRemovedFromUserEvent : UserLoggedEvent
	{
		public IncludeRemovedFromUserEvent(MagistrateUser user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
