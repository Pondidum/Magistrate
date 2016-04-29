using System;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Api;
using Magistrate.Domain;
using Magistrate.Domain.Commands;
using Magistrate.Domain.Services;
using Magistrate.Infrastructure;
using Magistrate.ReadModels;
using MediatR;
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

			var projectionist = container.GetInstance<Projectionist>();

			projectionist
				.Add(container.GetInstance<UserService>())
				.Add(container.GetInstance<RoleService>())
				.Add(container.GetInstance<PermissionService>())
				.Add(container.GetInstance<AllCollections>().Project);

			container
				.GetInstance<AggregateStore<Guid>>()
				.ReplayAll(MagistrateStream)
				.ForEach(projectionist.Apply);

			_container = container;
		}

		public void Configure(IAppBuilder app)
		{
			app.Use<MagistrateOperatorMiddleware>(_config);

			_container.GetInstance<PermissionsController>().Configure(app);
		}
	}
}
