using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserLoggedEvent : DomainEvent<Guid>
	{
		public Operator Operator { get; }
		public virtual string EventDescription { get { return ""; } }

		public UserLoggedEvent(Operator @operator)
		{
			Operator = @operator;
		}
	}
}
