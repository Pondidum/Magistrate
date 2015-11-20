using System;

namespace Magistrate.Domain.Events.SystemEvents
{
	public class PermissionAddedToSystemEvent : UserLoggedEvent
	{
		 public Guid PermissionID { get; set; }
	}
}
