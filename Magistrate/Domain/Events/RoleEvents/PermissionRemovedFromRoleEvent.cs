using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionRemovedFromRoleEvent : UserLoggedEvent
	{
		public PermissionRemovedFromRoleEvent(Operator @operator, Guid permissionID) : base(@operator)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }

		public override string EventDescription => $"Removed Permission from Role";
	}
}
