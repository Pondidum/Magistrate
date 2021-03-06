﻿using Ledger.Infrastructure;
using Magistrate.Api;
using Newtonsoft.Json;
using Owin;
using StructureMap;

namespace Magistrate
{
	public class MagistrateSystem
	{
		public const string MagistrateStream = "Magistrate";

		private readonly MagistrateConfiguration _config;
		private readonly Container _container;

		public MagistrateSystem(MagistrateConfiguration config)
		{
			_config = config;

			var container = new Container(new MagistrateRegistry(config.EventStore));

			container
				.GetInstance<Boot>()
				.Load();

			_container = container;
		}

		public void Configure(IAppBuilder app)
		{
			app.Use<SerilogMiddleware>();
			app.Use<ExceptionHandlerMiddleware>(_container.GetInstance<JsonSerializerSettings>());
			app.Use<MagistrateOperatorMiddleware>(_config);
			
			_container
				.GetAllInstances<IController>()
				.ForEach(controller => controller.Configure(app));
		}
	}
}
