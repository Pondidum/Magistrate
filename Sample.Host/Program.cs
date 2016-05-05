using System;
using System.IO;
using System.Security.Claims;
using Ledger.Stores;
using Ledger.Stores.Fs;
using Magistrate;
using Magistrate.WebInterface;
using Microsoft.Owin.Hosting;
using Serilog;

namespace Sample.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var host = WebApp.Start("http://localhost:4444", app =>
			{
				Log.Logger = new LoggerConfiguration()
					.MinimumLevel.Debug()
					.WriteTo.ColoredConsole()
					.CreateLogger();

				app.Use(typeof(SerilogMiddleware));

				//add a login provider here
				//app.Use<WindowsAuthentication>();

				Directory.CreateDirectory("store");

				var config = new MagistrateConfiguration
				{
					EventStore = new FileEventStore("store"),
					User = () =>
					{
						//e.g. take user from ClaimsPrincipal:

						//var current = ClaimsPrincipal.Current;
						//return new MagistrateUser
						//{
						//	Name = current.Identity.Name,
						//	Key = current.Identity.Name.ToLower().Replace(" ", "")
						//};

						return new Operator
						{
							Name = "Andy Dote",
							Key = "andy-dote",
							CanCreatePermissions = true,
							CanCreateRoles = true,
							CanCreateUsers = true,
						};
					}
				};


				var sys = new MagistrateSystem(config);
				sys.Configure(app);

				var ui = new MagistrateWebInterface();
				ui.Configure(app);

			});

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();

			host.Dispose();
		}
	}
}
