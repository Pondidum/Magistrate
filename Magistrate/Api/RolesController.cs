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
	public class RolesController : Controller
	{
		public RolesController(SystemFacade system)
			: base(system)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/roles/all").Get(GetAll);
			app.Route("/api/roles").Put(CreateRole);
			app.Route("/api/roles").Delete(DeleteRole);
			app.Route("/api/roles/{role-key}").Get(GetRoleDetails);
			app.Route("/api/roles/{role-key}/name").Put(UpdateRoleName);
			app.Route("/api/roles/{role-key}/description").Put(UpdateRoleDescription);
			app.Route("/api/roles/{role-key}/permissions").Put(AddPermissions);
			app.Route("/api/roles/{role-key}/permissions").Delete(RemovePermissions);
			app.Route("/api/roles/{role-key}/history").Get(GetHistory);

		}

		private async Task GetAll(IOwinContext context)
		{
			await context.JsonResponse(System.Roles);
		}

		private async Task CreateRole(IOwinContext context)
		{
			var dto = context.ReadJson<CreateRoleDto>();
			var role = System.CreateRole(context.GetUser(), dto.Key, dto.Name, dto.Description);

			await context.JsonResponse(role);
		}

		private async Task GetRoleDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async key =>
			{
				var role = System.Roles.FirstOrDefault(r => r.Key == key);

				if (role != null)
					await context.JsonResponse(role);
				else
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			});
		}

		private async Task DeleteRole(IOwinContext context)
		{
			var dto = context.ReadJson<RoleKey[]>();
			var user = context.GetUser();

			foreach (var key in dto)
			{
				System.OnRole(key, role => role.Deactivate(user));
			}

			await Task.Yield();
		}

		private async Task UpdateRoleName(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async key =>
			{
				var dto = context.ReadJson<EditRoleDto>();
				var user = context.GetUser();

				System.OnRole(key, role => role.ChangeName(user, dto.Name));

				await Task.Yield();
			});
		}

		private async Task UpdateRoleDescription(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async key =>
			{
				var dto = context.ReadJson<EditRoleDto>();
				var user = context.GetUser();

				System.OnRole(key, role => role.ChangeDescription(user, dto.Description));

				await Task.Yield();
			});
		}

		private async Task AddPermissions(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async roleKey =>
			{
				var dto = context.ReadJson<PermissionKey[]>();
				var user = context.GetUser();

				System.OnRole(roleKey, role =>
				{
					foreach (var permission in dto)
					{
						role.AddPermission(user, System.LoadPermission(permission));
					}
				});

				await Task.Yield();
			});
		}

		private async Task RemovePermissions(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async roleKey =>
			{
				var dto = context.ReadJson<PermissionKey[]>();
				var user = context.GetUser();

				System.OnRole(roleKey, role =>
				{
					foreach (var permission in dto)
					{
						role.RemovePermission(user, System.LoadPermission(permission));
					}
				});

				await Task.Yield();
			});
		}

		private async Task GetHistory(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async roleKey =>
			{
				var role = System.Roles.FirstOrDefault(u => u.Key == roleKey);

				if (role == null)
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
				else
					await context.JsonResponse(System.History.Where(h => h.OnAggregate == role.ID));
			});
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
