using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionAddedToRoleEvent : UserLoggedEvent
	{
		public PermissionAddedToRoleEvent(Operator @operator, Guid permissionID) : base(@operator)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
