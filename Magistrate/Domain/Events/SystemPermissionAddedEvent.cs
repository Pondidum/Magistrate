using System;

namespace Magistrate.Domain.Events
{
	public class SystemPermissionAddedEvent : UserLoggedEvent
	{
		 public Guid PermissionID { get; set; }
	}
}
