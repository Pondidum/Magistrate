using System;

namespace Magistrate.Domain.Events
{
	public class IncludeRemovedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
