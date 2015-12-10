using System.Linq;
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
			app.Route("/api/roles/{role-key}").Get(GetRoleDetails);
			app.Route("/api/roles/{role-key}").Delete(DeleteRole);
			app.Route("/api/roles/{role-key}/name").Put(UpdateRoleName);
			app.Route("/api/roles/{role-key}/description").Put(UpdateRoleDescription);
			app.Route("/api/roles/{role-key}/permissions").Put(AddPermissions);
			app.Route("/api/roles/{role-key}/permissions").Delete(RemovePermissions);
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
				var role = System.Roles.First(r => r.Key == key);
				await context.JsonResponse(role);
			});
		}

		private async Task DeleteRole(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async key =>
			{
				System.OnPermission(key, role => role.Deactivate(context.GetUser()));

				await Task.Yield();
			});
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
				var dto = context.ReadJson<string[]>();
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
				var dto = context.ReadJson<string[]>();
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

		private class CreateRoleDto
		{
			public string Key { get; set; }
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
