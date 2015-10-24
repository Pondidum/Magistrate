using Ledger;

namespace Magistrate.Domain.Events
{
	public class DescriptionChangedEvent : DomainEvent
	{
		public string NewDescription { get; set; }
	}
}
