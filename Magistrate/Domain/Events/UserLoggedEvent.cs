using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public abstract class UserLoggedEvent : DomainEvent<Guid>
	{
		public Operator Operator { get; }

		public UserLoggedEvent(Operator @operator)
		{
			Operator = @operator;
		}
	}
}
