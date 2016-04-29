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
			app.Route("/api/permissions/{permission-key}").Get(GetPermissionDetails);
			//app.Route("/api/permissions/{permission-key}/name").Put(UpdatePermissionName);
			//app.Route("/api/permissions/{permission-key}/description").Put(UpdatePermissionDescription);
			//app.Route("/api/permissions/{permission-key}/history").Get(GetHistory);
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

		//private async Task UpdatePermissionName(IOwinContext context)
		//{
		//	await NotFoundOrAction(context, PermissionKey, async key =>
		//	{
		//		var dto = context.ReadJson<EditPermissionDto>();
		//		var user = context.GetUser();

		//		System.OnPermission(key, permission => permission.ChangeName(user, dto.Name));

		//		await Task.Yield();
		//	});
		//}

		//private async Task UpdatePermissionDescription(IOwinContext context)
		//{
		//	await NotFoundOrAction(context, PermissionKey, async key =>
		//	{
		//		var dto = context.ReadJson<EditPermissionDto>();
		//		var user = context.GetUser();

		//		System.OnPermission(key, permission => permission.ChangeDescription(user, dto.Description));

		//		await Task.Yield();
		//	});
		//}

		//private async Task GetHistory(IOwinContext context)
		//{
		//	await NotFoundOrAction(context, PermissionKey, async permissionKey =>
		//	{
		//		var permission = System.Permissions.FirstOrDefault(u => u.Key == permissionKey);

		//		if (permission == null)
		//			context.Response.StatusCode = (int)HttpStatusCode.NotFound;
		//		else
		//			await context.JsonResponse(System.History.Where(h => h.OnAggregate == permission.ID));
		//	});
		//}

		private class CreatePermissionDto
		{
			public PermissionKey Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		//private class EditPermissionDto
		//{
		//	public string Name { get; set; }
		//	public string Description { get; set; }
		//}
	}
}
