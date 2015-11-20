using System;

namespace Magistrate.Domain.Events.SystemEvents
{
	public class RoleAddedToSystemEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}