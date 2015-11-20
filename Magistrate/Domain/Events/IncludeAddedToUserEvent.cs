using System;

namespace Magistrate.Domain.Events
{
	public class IncludeAddedToUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
