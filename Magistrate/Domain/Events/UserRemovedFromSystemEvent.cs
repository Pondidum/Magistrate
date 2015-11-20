using System;

namespace Magistrate.Domain.Events
{
	public class UserRemovedFromSystemEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
