using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class RoleAddedEvent : UserLoggedEvent
	{
		 public Guid RoleID { get; set; }
	}
}
