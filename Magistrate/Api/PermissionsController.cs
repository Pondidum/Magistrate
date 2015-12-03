using System.Linq;
using System.Threading.Tasks;
using Magistrate.Domain.Services;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class PermissionsController : Controller
	{
		public PermissionsController(SystemFacade system)
			: base(system)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/permissions/all").Get(GetAll);
			app.Route("/api/permissions").Put(CreatePermission);
			app.Route("/api/permissions/{permission-key}").Get(GetPermissionDetails);
			app.Route("/api/permissions/{permission-key}").Put(UpdatePermissionDetails);
			app.Route("/api/permissions/{permission-key}").Delete(DeletePermission);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.JsonResponse(System.Permissions);
		}

		private async Task CreatePermission(IOwinContext context)
		{
			var dto = context.ReadJson<CreatePermissionDto>();

			var permission = System.CreatePermission(context.GetUser(), dto.Key, dto.Name, dto.Description);

			await context.JsonResponse(permission);
		}

		private async Task GetPermissionDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, PermissionKey, async key =>
		   {
			   var permission = System.Permissions.First(p => p.Key == key);
			   await context.JsonResponse(permission);
		   });
		}

		private async Task DeletePermission(IOwinContext context)
		{
			await NotFoundOrAction(context, PermissionKey, async key =>
			{
				System.OnPermission(key, permission => permission.Deactivate(context.GetUser()));

				await Task.Yield();
			});
		}

		private async Task UpdatePermissionDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, PermissionKey, async key =>
			{
				var dto = context.ReadJson<EditPermissionDto>();
				var user = context.GetUser();

				System.OnPermission(key, permission =>
				{
					permission.ChangeName(user, dto.Name);
					permission.ChangeDescription(user, dto.Description);
				});

				await Task.Yield();

			});
		}

		private class CreatePermissionDto
		{
			public string Key { get; set; }
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
