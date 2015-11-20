using System;

namespace Magistrate.Domain.Events
{
	public class UserAddedToSystemEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}
