using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class PermissionAddedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; } 
	}
}
