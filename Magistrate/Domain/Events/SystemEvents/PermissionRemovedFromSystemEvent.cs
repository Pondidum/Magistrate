using System;

namespace Magistrate.Domain.Events.SystemEvents
{
	public class PermissionRemovedFromSystemEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}