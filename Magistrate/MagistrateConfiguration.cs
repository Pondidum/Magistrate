using System;
using Ledger;

namespace Magistrate
{
	public class MagistrateConfiguration
	{
		public Func<Operator> User { get; set; }
		public IEventStore EventStore { get; set; }
	}
}
