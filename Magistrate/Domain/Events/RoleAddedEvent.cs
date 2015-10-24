using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class RoleAddedEvent : DomainEvent
	{
		 public Guid RoleID { get; set; }
	}
}
