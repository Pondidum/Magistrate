using System;

namespace Magistrate.Domain.Events.SystemEvents
{
	public class RoleRemovedFromSystemEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}