using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeAddedToUserEvent : UserLoggedEvent
	{
		public IncludeAddedToUserEvent(Operator user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
