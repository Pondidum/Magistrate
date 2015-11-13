using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class RolesController : Controller
	{
		public RolesController(MagistrateSystem system)
			: base(system)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/roles/all").Get(GetAll);
			app.Route("/api/roles").Put(CreateRole);
			app.Route("/api/roles/{role-key}").Get(GetRoleDetails);
			app.Route("/api/roles/{role-key}/changeName").Put(ChangeName);
			app.Route("/api/roles/{role-key}/changeDescription").Put(ChangeDescription);
			app.Route("/api/roles/{role-key}/addPermission/{permission-key}").Put(AddPermission);
			app.Route("/api/roles/{role-key}/removePermission/{permission-key}").Put(RemovePermission);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.JsonResponse(System.Roles);
		}

		private async Task CreateRole(IOwinContext context)
		{
			var dto = context.ReadJson<CreateRoleDto>();
			var role = Role.Create(context.GetUser(), dto.Key, dto.Name, dto.Description);

			System.AddRole(context.GetUser(), role);
			System.Save();

			await context.JsonResponse(role);
		}

		private async Task GetRoleDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role => await context.JsonResponse(role));
		}

		private async Task ChangeName(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role =>
			{
				role.ChangeName(context.GetUser(), ReadBody(context));
				System.Save();

				await Task.Yield();
			});
		}

		private async Task ChangeDescription(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role =>
			{
				role.ChangeDescription(context.GetUser(), ReadBody(context));
				System.Save();

				await Task.Yield();
			});
		}

		private async Task AddPermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role =>
			{
				await NotFoundOrAction(context, GetPermission, async permission =>
				{
					role.AddPermission(context.GetUser(), permission);
					System.Save();

					await Task.Yield();
				});
			});
		}

		private async Task RemovePermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role =>
			{
				await NotFoundOrAction(context, GetPermission, async permission =>
				{
					role.RemovePermission(context.GetUser(), permission);
					System.Save();

					await Task.Yield();
				});
			});
		}

		private class CreateRoleDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
