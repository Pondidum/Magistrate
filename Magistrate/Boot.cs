using System;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Services;
using Magistrate.Infrastructure;
using Magistrate.ReadModels;

namespace Magistrate
{
	public class Boot
	{
		private readonly Projectionist _projectionist;
		private readonly AggregateStore<Guid> _store;

		public Boot(
			Projectionist projectionist,
			AggregateStore<Guid> store,
			UserService users,
			RoleService roles,
			PermissionService permissions,
			CollectionsReadModel collectionsReadModel,
			HistoryReadModel history,
			AuthorizationReadModel authorizationRead)
		{
			_projectionist = projectionist;
			_store = store;
			projectionist
				.Add(users)
				.Add(roles)
				.Add(permissions)
				.Add(collectionsReadModel.Project)
				.Add(history.Project)
				.Add(authorizationRead.Project);
		}

		public void Load()
		{
			_store
				.ReplayAll(MagistrateSystem.MagistrateStream)
				.ForEach(_projectionist.Apply);
		}
	}
}
