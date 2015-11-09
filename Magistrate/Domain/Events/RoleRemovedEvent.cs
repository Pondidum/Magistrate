using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class RoleRemovedEvent : UserLoggedEvent
	{
		public Guid RoleID { get; set; }
	}
}
