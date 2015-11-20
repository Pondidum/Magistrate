using System;

namespace Magistrate.Domain.Events
{
	public class SystemUserAddedEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
