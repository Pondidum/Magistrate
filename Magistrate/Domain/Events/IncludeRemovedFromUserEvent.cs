using System;

namespace Magistrate.Domain.Events
{
	public class IncludeRemovedFromUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
