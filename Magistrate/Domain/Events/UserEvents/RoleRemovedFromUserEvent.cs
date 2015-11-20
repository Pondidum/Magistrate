using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleRemovedFromUserEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}
