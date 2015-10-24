using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class PermissionAddedEvent : DomainEvent
	{
		public Guid PermissionID { get; set; } 
	}
}
