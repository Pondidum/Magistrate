using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeRemovedFromUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
