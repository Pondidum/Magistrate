using Magistrate.Api;
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
			app.Use<MagistrateOperatorMiddleware>(_config);

			_container.GetInstance<PermissionsController>().Configure(app);
		}
	}
}
