using System;
using Ledger;
using Ledger.Stores;
using Magistrate.Api;
using Magistrate.Domain;
using Microsoft.Owin.Hosting;

namespace Sample.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var eventStore = new AggregateStore<Guid>(new InMemoryEventStore<Guid>());
			var store = new Store(eventStore);
			var magistrate = new MagistrateTopware(store);

			var permission = Permission.Create("permission-one", "Permission One", "This does something, probably.");

			store.Save(permission);

            var host = WebApp.Start("http://localhost:4444", app =>
			{
				magistrate.Configure(app);
			});

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();

			host.Dispose();
		}
	}
}
