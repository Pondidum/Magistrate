using System;

namespace Magistrate.Domain.Events
{
	public class RoleRemovedFromSystemEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}