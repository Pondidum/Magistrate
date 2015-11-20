using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class RolePermissionAddedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; } 
	}
}
