using System;

namespace Magistrate.Domain.Events.SystemEvents
{
	public class UserRemovedFromSystemEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
