using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class RolePermissionRemovedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
