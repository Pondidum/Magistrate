using Ledger;

namespace Magistrate.Domain.Events
{
	public class NameChangedEvent : UserLoggedEvent
	{
		 public string NewName { get; set; }
	}
}
