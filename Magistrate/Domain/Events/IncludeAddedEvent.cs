using System;

namespace Magistrate.Domain.Events
{
	public class IncludeAddedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
