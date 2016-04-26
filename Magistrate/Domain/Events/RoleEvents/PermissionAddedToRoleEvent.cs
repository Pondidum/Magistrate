using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionAddedToRoleEvent : UserLoggedEvent
	{
		public PermissionAddedToRoleEvent(Operator user, Guid permissionID, string permissionName, string roleName) : base(user)
		{
			PermissionID = permissionID;
			PermissionName = permissionName;
			RoleName = roleName;
		}

		public Guid PermissionID { get; }
		public string PermissionName { get; }
		public string RoleName { get; }

		public override string EventDescription => $"Added {PermissionName} to Role '{RoleName}'";
	}
}
