using Ledger;

namespace Magistrate.Domain.Events
{
	public class NameChangedEvent : DomainEvent
	{
		 public string NewName { get; set; }
	}
}
