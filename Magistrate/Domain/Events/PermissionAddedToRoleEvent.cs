using System;

namespace Magistrate.Domain.Events
{
	public class PermissionAddedToRoleEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; } 
	}
}
