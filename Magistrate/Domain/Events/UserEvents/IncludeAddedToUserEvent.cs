using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeAddedToUserEvent : UserLoggedEvent
	{
		public IncludeAddedToUserEvent(Operator @operator, Guid permissionID) : base(@operator)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
