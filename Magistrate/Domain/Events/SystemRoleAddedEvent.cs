using System;

namespace Magistrate.Domain.Events
{
	public class SystemRoleAddedEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}