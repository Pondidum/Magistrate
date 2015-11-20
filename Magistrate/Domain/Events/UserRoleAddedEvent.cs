using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserRoleAddedEvent : UserLoggedEvent
	{
		 public Guid RoleID { get; set; }
	}
}
