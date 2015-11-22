using AutoMapper;
using Magistrate.Api.Responses;
using Magistrate.Domain.ReadModels;
using Magistrate.Domain.Services;
using Owin;

namespace Magistrate.Api
{
	public class MagistrateApi
	{
		private readonly MagistrateConfiguration _config;
		private readonly PermissionsController _permissions;
		private readonly RolesController _roles;
		private readonly UsersController _users;
		private readonly SystemFacade _system;

		public MagistrateApi(MagistrateConfiguration config)
		{
			_config = config;
			_system	= new SystemFacade(config.EventStore);

			_permissions = new PermissionsController(_system);
			_roles = new RolesController(_system);
			_users = new UsersController(_system);

			ConfigureAutoMapper();
		}

		private void ConfigureAutoMapper()
		{
			Mapper.CreateMap<PermissionReadModel, PermissionResponse>();
			Mapper.CreateMap<RoleReadModel, RoleResponse>();
			Mapper.CreateMap<UserReadModel, UserResponse>();
		}

		public void Configure(IAppBuilder app)
		{
			app.Use<JsonExceptionMiddleware>();

			if (_config.User != null)
				app.Use<MagistrateUserMiddleware>(_config);

			_permissions.Configure(app);
			_roles.Configure(app);
			_users.Configure(app);
		}
	}
}
