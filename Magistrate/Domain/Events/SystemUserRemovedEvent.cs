using System;

namespace Magistrate.Domain.Events
{
	public class SystemUserRemovedEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
