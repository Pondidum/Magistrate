using Magistrate.Domain;
using Owin;

namespace Magistrate.Api
{
	public class MagistrateTopware
	{
		private readonly PermissionsController _permissions;
		private readonly RolesController _roles;
		private readonly UsersController _users;

		public MagistrateTopware(Store store)
		{
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
