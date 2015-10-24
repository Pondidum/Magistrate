using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class PermissionRemovedEvent : DomainEvent
	{
		public Guid PermissionID { get; set; }
	}
}
