using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionRemovedFromRoleEvent : UserLoggedEvent
	{
		public PermissionRemovedFromRoleEvent(MagistrateUser user, Guid permissionID, string permissionName, string roleName) : base(user)
		{
			PermissionID = permissionID;
			PermissionName = permissionName;
			RoleName = roleName;
		}

		public Guid PermissionID { get; }
		public string PermissionName { get; }
		public string RoleName { get; }

		public override string EventDescription => $"Removed {PermissionName} from Role '{RoleName}'";
	}
}
