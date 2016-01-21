using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionRemovedFromRoleEvent : UserLoggedEvent
	{
		public PermissionRemovedFromRoleEvent(MagistrateUser user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; }
	}
}
