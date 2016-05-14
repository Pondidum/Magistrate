using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ledger.Infrastructure;
using Magistrate.Domain;
using Magistrate.Domain.Commands;
using Magistrate.ReadModels;
using MediatR;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class UsersController : IController
	{
		private readonly CollectionsReadModel _collectionsReadModel;
		private readonly AuthorizationReadModel _authReadModel;
		private readonly JsonSerializerSettings _settings;
		private readonly IMediator _mediator;

		public UsersController(CollectionsReadModel collectionsReadModel, AuthorizationReadModel authReadModel, JsonSerializerSettings settings, IMediator mediator)
		{
			_collectionsReadModel = collectionsReadModel;
			_authReadModel = authReadModel;
			_settings = settings;
			_mediator = mediator;
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/users").Get(GetAll);
			app.Route("/api/users").Put(CreateUser);
			app.Route("/api/users").Delete(DeleteUser);

			app.Route("/api/users/{key}").Get(GetUserDetails);
			app.Route("/api/users/{key}/name").Put(UpdateUserName);

			app.Route("/api/users/{key}/includes").Put(AddInclude);
			app.Route("/api/users/{key}/includes").Delete(RemoveInclude);

			app.Route("/api/users/{key}/revokes").Put(AddRevoke);
			app.Route("/api/users/{key}/revokes").Delete(RemoveRevoke);

			app.Route("/api/users/{key}/roles").Put(AddRole);
			app.Route("/api/users/{key}/roles").Delete(RemoveRole);

			app.Route("/api/users/{user-key}/check/{permission-key}").Get(CheckPermission);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.WriteJson(_collectionsReadModel.Users, _settings);
		}

		private async Task GetUserDetails(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			await context.WriteJson(user, _settings);
		}

		private async Task CreateUser(IOwinContext context)
		{
			var dto = context.ReadJson<CreateUserDto>();

			_mediator.Publish(new CreateUserCommand(
				context.GetOperator(),
				dto.Key,
				dto.Name
			));

			await Task.Yield();
		}

		private async Task DeleteUser(IOwinContext context)
		{
			var op= context.GetOperator();

			context
				.ReadJson<UserKey[]>()
				.Select(key => _collectionsReadModel.Users.Single(p => p.Key == key))
				.ForEach(u => _mediator.Publish(new DeleteUserCommand(op, u.ID)));

			await Task.Yield();
		}

		private async Task UpdateUserName(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var dto = context.ReadJson<EditUserDto>();

			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			_mediator.Publish(new ChangeUserNameCommand(
				context.GetOperator(),
				user.ID,
				dto.Name
			));

			await Task.Yield();
		}


		private async Task AddInclude(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<PermissionKey[]>()
				.Select(pk => _collectionsReadModel.Permissions.Single(p => p.Key == pk))
				.Select(p => p.ID);

			_mediator.Publish(new AddIncludesToUserCommand(
				context.GetOperator(),
				user.ID,
				permissions
			));

			await Task.Yield();
		}

		private async Task RemoveInclude(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<PermissionKey[]>()
				.Select(pk => _collectionsReadModel.Permissions.Single(p => p.Key == pk))
				.Select(p => p.ID);

			_mediator.Publish(new RemoveIncludesFromUserCommand(
				context.GetOperator(),
				user.ID,
				permissions
			));

			await Task.Yield();
		}

		private async Task AddRevoke(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<PermissionKey[]>()
				.Select(pk => _collectionsReadModel.Permissions.Single(p => p.Key == pk))
				.Select(p => p.ID);

			_mediator.Publish(new AddRevokesToUserCommand(
				context.GetOperator(),
				user.ID,
				permissions
			));

			await Task.Yield();
		}

		private async Task RemoveRevoke(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<PermissionKey[]>()
				.Select(pk => _collectionsReadModel.Permissions.Single(p => p.Key == pk))
				.Select(p => p.ID);

			_mediator.Publish(new RemoveRevokesFromUserCommand(
				context.GetOperator(),
				user.ID,
				permissions
			));

			await Task.Yield();
		}

		private async Task AddRole(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<RoleKey[]>()
				.Select(rk => _collectionsReadModel.Roles.Single(p => p.Key == rk))
				.Select(r => r.ID);

			_mediator.Publish(new AddRolesToUserCommand(
				context.GetOperator(),
				user.ID,
				permissions
			));

			await Task.Yield();
		}

		private async Task RemoveRole(IOwinContext context)
		{
			var key = new UserKey(context.GetRouteValue("key"));
			var user = _collectionsReadModel.Users.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<RoleKey[]>()
				.Select(rk => _collectionsReadModel.Roles.Single(p => p.Key == rk))
				.Select(r => r.ID);

			_mediator.Publish(new RemoveRolesFromUserCommand(
				context.GetOperator(),
				user.ID,
				permissions
			));

			await Task.Yield();
		}

		private async Task CheckPermission(IOwinContext context)
		{
			var userKey = new UserKey(context.GetRouteValue("user-key"));
			var permissionKey = new PermissionKey(context.GetRouteValue("permission-key"));

			var user = _collectionsReadModel.Users.Single(u => u.Key == userKey);
			var permission = _collectionsReadModel.Permissions.Single(p => p.Key == permissionKey);

			var result = new
			{
				Allowed = _authReadModel.CanUserPerformAction(user.ID, permission.ID)
			};

			await context.WriteJson(result, _settings);
		}

		private class CreateUserDto
		{
			public UserKey Key { get; set; }
			public string Name { get; set; }
		}

		private class EditUserDto
		{
			public string Name { get; set; }
		}
	}
}
