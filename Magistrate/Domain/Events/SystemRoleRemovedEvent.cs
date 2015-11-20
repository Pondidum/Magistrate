using System;

namespace Magistrate.Domain.Events
{
	public class SystemRoleRemovedEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}