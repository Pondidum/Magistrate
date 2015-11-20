using System;

namespace Magistrate.Domain.Events
{
	public class PermissionRemovedFromRoleEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
