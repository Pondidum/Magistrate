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

		public PermissionsReadModel Permissions { get; }
		public UsersReadModel Users { get; }

		public Store(AggregateStore<Guid> store)
		{
			_store = store;
			_projections = new Projection();

			Permissions = new PermissionsReadModel();
			Users = new UsersReadModel();

			RegisterProjections();
		}

		private void RegisterProjections()
		{
			_projections.Register<Permission>(Permissions.ProjectTo);
			_projections.Register<User>(Users.ProjectTo);
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

		public void Save(User user)
		{
			_store.Save(user);
			_projections.Run(user);
		}
	}
}
