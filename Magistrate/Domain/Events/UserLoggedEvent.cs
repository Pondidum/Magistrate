using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserLoggedEvent : DomainEvent
	{
		public MagistrateUser User { get; set; }
	}
}
