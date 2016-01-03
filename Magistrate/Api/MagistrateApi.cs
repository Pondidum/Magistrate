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
		private readonly HistoryController _history;

		public MagistrateApi(MagistrateConfiguration config)
		{
			_config = config;
			_system	= new SystemFacade(config.EventStore);

			_permissions = new PermissionsController(_system);
			_roles = new RolesController(_system);
			_users = new UsersController(_system);
			_history = new HistoryController(_system);

			ConfigureAutoMapper();

			_system.Load();
		}

		private void ConfigureAutoMapper()
		{
			Mapper.CreateMap<PermissionReadModel, PermissionResponse>();
			Mapper.CreateMap<RoleReadModel, RoleResponse>();
			Mapper.CreateMap<UserReadModel, UserResponse>();
			Mapper.CreateMap<HistoryEntry, HistoryResponse>();
		}

		public void Configure(IAppBuilder app)
		{
			app.Use<JsonExceptionMiddleware>();

			if (_config.User != null)
				app.Use<MagistrateUserMiddleware>(_config);

			_permissions.Configure(app);
			_roles.Configure(app);
			_users.Configure(app);
			_history.Configure(app);
		}
	}
}
