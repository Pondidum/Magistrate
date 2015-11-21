using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserLoggedEvent : DomainEvent<Guid>
	{
		public MagistrateUser User { get; set; }
	}
}
