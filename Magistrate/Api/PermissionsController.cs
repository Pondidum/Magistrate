using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Magistrate.Domain;
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
			app.Route("/api/permissions/").Delete(DeletePermission);
			app.Route("/api/permissions/{permission-key}").Get(GetPermissionDetails);
			app.Route("/api/permissions/{permission-key}/name").Put(UpdatePermissionName);
			app.Route("/api/permissions/{permission-key}/description").Put(UpdatePermissionDescription);
			app.Route("/api/permissions/{permission-key}/history").Get(GetHistory);
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
				var permission = System.Permissions.FirstOrDefault(p => p.Key == key);

				if (permission != null)
					await context.JsonResponse(permission);
				else
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			});
		}

		private async Task DeletePermission(IOwinContext context)
		{
			var dto = context.ReadJson<PermissionKey[]>();
			var user = context.GetUser();

			foreach (var key in dto)
			{
				System.OnPermission(key, permission => permission.Deactivate(user));
			}

			await Task.Yield();
		}

		private async Task UpdatePermissionName(IOwinContext context)
		{
			await NotFoundOrAction(context, PermissionKey, async key =>
			{
				var dto = context.ReadJson<EditPermissionDto>();
				var user = context.GetUser();

				System.OnPermission(key, permission => permission.ChangeName(user, dto.Name));

				await Task.Yield();
			});
		}

		private async Task UpdatePermissionDescription(IOwinContext context)
		{
			await NotFoundOrAction(context, PermissionKey, async key =>
			{
				var dto = context.ReadJson<EditPermissionDto>();
				var user = context.GetUser();

				System.OnPermission(key, permission => permission.ChangeDescription(user, dto.Description));

				await Task.Yield();
			});
		}

		private async Task GetHistory(IOwinContext context)
		{
			await NotFoundOrAction(context, PermissionKey, async permissionKey =>
			{
				var permission = System.Permissions.FirstOrDefault(u => u.Key == permissionKey);

				if (permission == null)
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
				else
					await context.JsonResponse(System.History.Where(h => h.OnAggregate == permission.ID));
			});
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
