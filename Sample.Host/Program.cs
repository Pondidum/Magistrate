using System;
using System.Security.Claims;
using Ledger.Stores;
using Magistrate;
using Microsoft.Owin.Hosting;

namespace Sample.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var host = WebApp.Start("http://localhost:4444", app =>
			{
				//add a login provider here
				//app.Use<WindowsAuthentication>();

				app.UseMagistrateApi(config =>
				{
					config.EventStore = new InMemoryEventStore<Guid>();
					config.User = () =>
					{
						//e.g. take user from ClaimsPrincipal:

						//var current = ClaimsPrincipal.Current;
						//return new MagistrateUser
						//{
						//	Name = current.Identity.Name,
						//	Key = current.Identity.Name.ToLower().Replace(" ", "")
						//};

						return new MagistrateUser
						{
							Name = "Andy Dote",
							Key = "andy-dote"
						};
					};
				});

				var ui = new MagistrateWebInterface();
				
				ui.Configure(app);

			});

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();

			host.Dispose();
		}
	}
}
