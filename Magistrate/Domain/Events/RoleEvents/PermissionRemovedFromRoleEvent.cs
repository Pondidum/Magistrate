using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionRemovedFromRoleEvent : UserLoggedEvent
	{
		public PermissionRemovedFromRoleEvent(Operator user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }

		public override string EventDescription => $"Removed Permission from Role";
	}
}
