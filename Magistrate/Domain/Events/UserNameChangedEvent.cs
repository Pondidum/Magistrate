using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserNameChangedEvent : UserLoggedEvent
	{
		 public string NewName { get; set; }
	}
}
