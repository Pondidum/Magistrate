using System;
using Magistrate.Api;
using Owin;

namespace Magistrate
{
	public static class MagistrateExtensions
	{
		public static void UseMagistrateApi(this IAppBuilder app, Action<MagistrateConfiguration> configAction)
		{
			var config = new MagistrateConfiguration();
			configAction(config);

			app.UseMagistrateApi(config);
		}

		public static void UseMagistrateApi(this IAppBuilder app, MagistrateConfiguration config)
		{
			var magistrate = new MagistrateApi(config);
			magistrate.Configure(app);
		}
	}
}
