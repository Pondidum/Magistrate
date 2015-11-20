using System;

namespace Magistrate.Domain.Events
{
	public class UserRoleRemovedEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}
