using System.Linq;
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
	public class PermissionsController
	{
		private readonly AllCollections _allCollections;
		private readonly JsonSerializerSettings _settings;
		private readonly IMediator _mediator;

		public PermissionsController(AllCollections allCollections, JsonSerializerSettings settings, IMediator mediator)
		{
			_allCollections = allCollections;
			_settings = settings;
			_mediator = mediator;
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/permissions").Get(GetAll);
			app.Route("/api/permissions").Put(CreatePermission);
			app.Route("/api/permissions/").Delete(DeletePermission);
			app.Route("/api/permissions/{key}").Get(GetPermissionDetails);
			app.Route("/api/permissions/{key}/name").Put(UpdatePermissionName);
			app.Route("/api/permissions/{key}/description").Put(UpdatePermissionDescription);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.WriteJson(_allCollections.Permissions, _settings);
		}

		private async Task CreatePermission(IOwinContext context)
		{
			var dto = context.ReadJson<CreatePermissionDto>();

			_mediator.Publish(new CreatePermissionCommand(
				context.GetOperator(),
				dto.Key,
				dto.Name,
				dto.Description
			));

			await Task.Yield();
		}

		private async Task GetPermissionDetails(IOwinContext context)
		{
			var key = new PermissionKey(context.GetRouteValue("key"));
			var permission = _allCollections.Permissions.Single(p => p.Key == key);

			await context.WriteJson(permission, _settings);
		}

		private async Task DeletePermission(IOwinContext context)
		{
			var user = context.GetOperator();

			context
				.ReadJson<PermissionKey[]>()
				.Select(key => _allCollections.Permissions.Single(p => p.Key == key))
				.ForEach(p => _mediator.Publish(new DeletePermissionCommand(user, p.ID)));

			await Task.Yield();
		}

		private async Task UpdatePermissionName(IOwinContext context)
		{
			var key = new PermissionKey(context.GetRouteValue("key"));
			var dto = context.ReadJson<EditPermissionDto>();

			var permission = _allCollections.Permissions.Single(p => p.Key == key);

			_mediator.Publish(new ChangePermissionNameCommand(
				context.GetOperator(),
				permission.ID,
				dto.Name
			));

			await Task.Yield();
		}

		private async Task UpdatePermissionDescription(IOwinContext context)
		{
			var key = new PermissionKey(context.GetRouteValue("key"));
			var dto = context.ReadJson<EditPermissionDto>();

			var permission = _allCollections.Permissions.Single(p => p.Key == key);

			_mediator.Publish(new ChangePermissionDescriptionCommand(
				context.GetOperator(),
				permission.ID,
				dto.Description
			));

			await Task.Yield();
		}

		private class CreatePermissionDto
		{
			public PermissionKey Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		private class EditPermissionDto
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
