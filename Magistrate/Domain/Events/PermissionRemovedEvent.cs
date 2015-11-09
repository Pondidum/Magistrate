using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class PermissionRemovedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
