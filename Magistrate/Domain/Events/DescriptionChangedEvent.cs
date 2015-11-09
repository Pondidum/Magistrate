using Ledger;

namespace Magistrate.Domain.Events
{
	public class DescriptionChangedEvent : UserLoggedEvent
	{
		public string NewDescription { get; set; }
	}
}
