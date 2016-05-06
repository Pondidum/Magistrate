using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeRemovedFromUserEvent : UserLoggedEvent
	{
		public IncludeRemovedFromUserEvent(Operator @operator, Guid permissionID) : base(@operator)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
