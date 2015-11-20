using System;

namespace Magistrate.Domain.Events.SystemEvents
{
	public class UserAddedToSystemEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
