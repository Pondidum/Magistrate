using System;

namespace Magistrate.Domain.Events
{
	public class PermissionRemovedFromSystemEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}