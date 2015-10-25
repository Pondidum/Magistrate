using System;
using Ledger;
using Magistrate.Infrastructure;
using Magistrate.ReadModels;

namespace Magistrate.Domain
{
	public class Store
	{
		private readonly AggregateStore<Guid> _store;
		private readonly Projection _projections;

		private readonly PermissionsReadModel _permissions;

		public Store(AggregateStore<Guid> store)
		{
			_store = store;
			_projections = new Projection();

			_permissions = new PermissionsReadModel();

			RegisterProjections();
		}

		private void RegisterProjections()
		{
			_projections.Register<Permission>(permission => _permissions.AllPermissions[permission.ID] = permission.Name);
		}

		public void LoadExistingReadModels()
		{
			// ?????????
		}

		public void Save(Permission permission)
		{
			_store.Save(permission);
			_projections.Run(permission);
		}
	}
}
