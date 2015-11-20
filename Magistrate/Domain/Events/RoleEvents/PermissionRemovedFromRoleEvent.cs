using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class PermissionRemovedFromRoleEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
