using System;
using Ledger;
using Magistrate.Domain;
using Owin;

namespace Magistrate.Api
{
	public class MagistrateApi
	{
		private readonly PermissionsController _permissions;
		private readonly RolesController _roles;
		private readonly UsersController _users;
		
		public MagistrateApi(MagistrateConfiguration config)
		{
			var aggregateStore = new AggregateStore<Guid>(config.EventStore);
			var store = new Store(aggregateStore);

			_permissions = new PermissionsController(store);
			_roles = new RolesController(store);
			_users = new UsersController(store);
		}

		public void Configure(IAppBuilder app)
		{
			_permissions.Configure(app);
			_roles.Configure(app);
			_users.Configure(app);
		}
	}
}
