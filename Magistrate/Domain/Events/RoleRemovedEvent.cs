using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class RoleRemovedEvent : DomainEvent
	{
		public Guid RoleID { get; set; }
	}
}
