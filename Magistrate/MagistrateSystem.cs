using System;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Services;
using Magistrate.Infrastructure;
using Magistrate.ReadModels;

namespace Magistrate
{
	public class MagistrateSystem
	{
		public const string MagistrateStream = "Magistrate";

		public MagistrateSystem(MagistrateConfiguration config)
		{

			var all = new AllCollections();
			var userService = new UserService();

			var projectionist = new Projectionist()
				.Add(all.Project)
				.Add(userService.Project);

			var projectionStore = new ProjectionStore(config.EventStore, projectionist.Apply);
			var store = new AggregateStore<Guid>(projectionStore);

			store
				.ReplayAll(MagistrateStream)
				.ForEach(projectionist.Apply);
		}
	}
}
