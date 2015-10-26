using System;
using System.Security.Claims;
using Ledger.Stores;
using Magistrate;
using Magistrate.Api;
using Microsoft.Owin.Hosting;

namespace Sample.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = new MagistrateConfiguration
			{
				EventStore = new InMemoryEventStore<Guid>(),
				User = () =>
				{
					var current = ClaimsPrincipal.Current;

					return new MagistrateUser
					{
						Name = current.Identity.Name,
						Key = current.FindFirst("userID").Value
					};
				}
			};

			var magistrate = new MagistrateApi(config);


			var host = WebApp.Start("http://localhost:4444", app =>
			{
				//add a login provider here
				//app.Use<WindowsAuthentication>();


				magistrate.Configure(app);
			});

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();

			host.Dispose();
		}
	}
}
