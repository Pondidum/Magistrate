using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserLoggedEvent : DomainEvent<Guid>
	{
		public MagistrateUser User { get; }
		public virtual string EventDescription { get { return ""; } }

		public UserLoggedEvent(MagistrateUser user)
		{
			User = user;
		}
	}
}
