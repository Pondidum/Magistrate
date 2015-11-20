using System;

namespace Magistrate.Domain.Events
{
	public class RoleAddedToSystemEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}