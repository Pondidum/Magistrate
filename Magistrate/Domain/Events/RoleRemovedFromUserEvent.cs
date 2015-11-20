using System;

namespace Magistrate.Domain.Events
{
	public class RoleRemovedFromUserEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}
