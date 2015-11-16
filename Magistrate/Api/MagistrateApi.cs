using System;
using System.Linq;
using AutoMapper;
using Ledger;
using Magistrate.Api.Responses;
using Magistrate.Domain;
using Owin;

namespace Magistrate.Api
{
	public class MagistrateApi
	{
		private readonly MagistrateConfiguration _config;
		private readonly PermissionsController _permissions;
		private readonly RolesController _roles;
		private readonly UsersController _users;
		private readonly MagistrateSystem _system;

		public MagistrateApi(MagistrateConfiguration config)
		{
			_config = config;
			var aggregateStore = new AggregateStore<Guid>(config.EventStore);
			_system = new MagistrateSystem(aggregateStore);

			_permissions = new PermissionsController(_system);
			_roles = new RolesController(_system);
			_users = new UsersController(_system);

			ConfigureAutoMapper();
		}

		private void ConfigureAutoMapper()
		{
			Mapper.CreateMap<User, UserResponse>();
			Mapper.CreateMap<Role, RoleResponse>();
			Mapper.CreateMap<Permission, PermissionResponse>();

			Mapper
				.CreateMap<Guid, RoleResponse>()
				.ConvertUsing(id => _system.Roles.Single(r => r.ID == id).Map<RoleResponse>());

			Mapper
				.CreateMap<Guid, PermissionResponse>()
				.ConvertUsing(id => _system.Permissions.Single(r => r.ID == id).Map<PermissionResponse>());
		}

		public void Configure(IAppBuilder app)
		{
			app.Use<JsonExceptionMiddleware>();
			app.Use<SaveStoreMiddleware>(_system);

			if (_config.User != null)
				app.Use<MagistrateUserMiddleware>(_config);

			_permissions.Configure(app);
			_roles.Configure(app);
			_users.Configure(app);
		}
	}
}
