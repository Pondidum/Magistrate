using System.Threading.Tasks;
using Magistrate.Domain;
using Magistrate.Infrastructure;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class PermissionsController : Controller
	{
		public PermissionsController(Store store)
			: base(store)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/permissions/all").Get(GetAll);
			app.Route("/api/permissions").Put(CreatePermission);
			app.Route("/api/permissions/{permission-key}").Get(GetPermissionDetails);
			app.Route("/api/permissions/{permission-key}/changeName").Put(ChangeName);
			app.Route("/api/permissions/{permission-key}/changeDescription").Put(ChangeDescription);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.JsonResponse(Store.Permissions.AllPermissions);
		}
		private async Task CreatePermission(IOwinContext context)
		{
			var dto = context.ReadJson<CreatePermissionDto>();
			Store.Save(Permission.Create(dto.Key, dto.Name, dto.Description));
			await Task.Yield();
		}

		private async Task GetPermissionDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, GetPermission, async permission => await context.JsonResponse(permission));
		}

		private async Task ChangeName(IOwinContext context)
		{
			await NotFoundOrAction(context, GetPermission, async permission =>
			{
				permission.ChangeName(ReadBody(context));
				Store.Save(permission);

				await Task.Yield();
			});
		}

		private async Task ChangeDescription(IOwinContext context)
		{
			await NotFoundOrAction(context, GetPermission, async permission =>
			{
				permission.ChangeDescription(ReadBody(context));
				Store.Save(permission);

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
