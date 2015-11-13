using System.Threading.Tasks;
using Magistrate.Api.Responses;
using Magistrate.Domain;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class PermissionsController : Controller
	{
		public PermissionsController(MagistrateSystem system)
			: base(system)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/permissions/all").Get(GetAll);
			app.Route("/api/permissions").Put(CreatePermission);
			app.Route("/api/permissions/{permission-key}").Get(GetPermissionDetails);
			app.Route("/api/permissions/{permission-key}").Delete(DeletePermission);
			app.Route("/api/permissions/{permission-key}/changeName").Put(ChangeName);
			app.Route("/api/permissions/{permission-key}/changeDescription").Put(ChangeDescription);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.JsonResponse(System.Permissions);
		}

		private async Task CreatePermission(IOwinContext context)
		{
			var dto = context.ReadJson<CreatePermissionDto>();
			var permission = Permission.Create(context.GetUser(), dto.Key, dto.Name, dto.Description);

			System.AddPermission(context.GetUser(), permission);

			await context.JsonResponse(permission);
		}

		private async Task GetPermissionDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, GetPermission, async permission => await context.JsonResponse(permission));
		}

		private async Task DeletePermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetPermission, async permission =>
			{
				System.RemovePermission(context.GetUser(), permission);

				await Task.Yield();
			});

		}

		private async Task ChangeName(IOwinContext context)
		{
			await NotFoundOrAction(context, GetPermission, async permission =>
			{
				permission.ChangeName(context.GetUser(), ReadBody(context));

				await Task.Yield();
			});
		}

		private async Task ChangeDescription(IOwinContext context)
		{
			await NotFoundOrAction(context, GetPermission, async permission =>
			{
				permission.ChangeDescription(context.GetUser(), ReadBody(context));

				await Task.Yield();
			});
		}

		private class CreatePermissionDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
