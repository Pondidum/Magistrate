using System;

namespace Magistrate.Domain.Events
{
	public class UserRemovedEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
