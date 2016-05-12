using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ledger.Infrastructure;
using Magistrate.Domain;
using Magistrate.Domain.Commands;
using Magistrate.Domain.Services;
using Magistrate.ReadModels;
using MediatR;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class RolesController
	{
		private readonly CollectionsReadModel _collectionsReadModel;
		private readonly JsonSerializerSettings _settings;
		private readonly IMediator _mediator;

		public RolesController(CollectionsReadModel collectionsReadModel, JsonSerializerSettings settings, IMediator mediator)
		{
			_collectionsReadModel = collectionsReadModel;
			_settings = settings;
			_mediator = mediator;
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/roles").Get(GetAll);
			app.Route("/api/roles").Put(CreateRole);
			app.Route("/api/roles").Delete(DeleteRole);
			app.Route("/api/roles/{key}").Get(GetRoleDetails);
			app.Route("/api/roles/{key}/name").Put(UpdateRoleName);
			app.Route("/api/roles/{key}/description").Put(UpdateRoleDescription);
			app.Route("/api/roles/{key}/permissions").Put(AddPermissions);
			app.Route("/api/roles/{key}/permissions").Delete(RemovePermissions);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.WriteJson(_collectionsReadModel.Roles, _settings);
		}

		private async Task GetRoleDetails(IOwinContext context)
		{
			var key = new RoleKey(context.GetRouteValue("key"));
			var role = _collectionsReadModel.Roles.Single(r => r.Key == key);

			await context.WriteJson(role, _settings);
		}

		private async Task CreateRole(IOwinContext context)
		{
			var dto = context.ReadJson<CreateRoleDto>();

			_mediator.Publish(new CreateRoleCommand(
				context.GetOperator(),
				dto.Key,
				dto.Name,
				dto.Description
			));

			await Task.Yield();
		}

		private async Task DeleteRole(IOwinContext context)
		{
			var user = context.GetOperator();

			context
				.ReadJson<RoleKey[]>()
				.Select(key => _collectionsReadModel.Roles.Single(p => p.Key == key))
				.ForEach(p => _mediator.Publish(new DeleteRoleCommand(user, p.ID)));

			await Task.Yield();
		}

		private async Task UpdateRoleName(IOwinContext context)
		{
			var key = new RoleKey(context.GetRouteValue("key"));
			var dto = context.ReadJson<EditRoleDto>();

			var role = _collectionsReadModel.Roles.Single(r => r.Key == key);

			_mediator.Publish(new ChangeRoleNameCommand(
				context.GetOperator(),
				role.ID,
				dto.Name
			));

			await Task.Yield();
		}

		private async Task UpdateRoleDescription(IOwinContext context)
		{
			var key = new RoleKey(context.GetRouteValue("key"));
			var dto = context.ReadJson<EditRoleDto>();

			var role = _collectionsReadModel.Roles.Single(r => r.Key == key);

			_mediator.Publish(new ChangeRoleDescriptionCommand(
				context.GetOperator(),
				role.ID,
				dto.Description
			));

			await Task.Yield();
		}

		private async Task AddPermissions(IOwinContext context)
		{
			var key = new RoleKey(context.GetRouteValue("key"));
			var role = _collectionsReadModel.Roles.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<PermissionKey[]>()
				.Select(pk => _collectionsReadModel.Permissions.Single(p => p.Key == pk))
				.Select(p => p.ID);

			_mediator.Publish(new AddPermissionsToRoleCommand(
				context.GetOperator(),
				role.ID,
				permissions
			));

			await Task.Yield();
		}

		private async Task RemovePermissions(IOwinContext context)
		{
			var key = new RoleKey(context.GetRouteValue("key"));
			var role = _collectionsReadModel.Roles.Single(r => r.Key == key);

			var permissions = context
				.ReadJson<PermissionKey[]>()
				.Select(pk => _collectionsReadModel.Permissions.Single(p => p.Key == pk))
				.Select(p => p.ID);

			_mediator.Publish(new RemovePermissionsFromRoleCommand(
				context.GetOperator(),
				role.ID,
				permissions
			));

			await Task.Yield();
		}

		private class CreateRoleDto
		{
			public RoleKey Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		private class EditRoleDto
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
