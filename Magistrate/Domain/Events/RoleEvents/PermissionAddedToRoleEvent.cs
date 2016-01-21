using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionAddedToRoleEvent : UserLoggedEvent
	{
		public PermissionAddedToRoleEvent(MagistrateUser user, Guid permissionID) : base(user)
		{
			PermissionID = permissionID;
		}

		public Guid PermissionID { get; } 
	}
}
