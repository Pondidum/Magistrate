using System;

namespace Magistrate.Domain.Events
{
	public class UserAddedEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}

	public class UserRemovedEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
