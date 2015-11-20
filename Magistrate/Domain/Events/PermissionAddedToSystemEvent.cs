using System;

namespace Magistrate.Domain.Events
{
	public class PermissionAddedToSystemEvent : UserLoggedEvent
	{
		 public Guid PermissionID { get; set; }
	}
}
