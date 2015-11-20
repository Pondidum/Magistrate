using System;

namespace Magistrate.Domain.Events
{
	public class SystemPermissionRemovedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}