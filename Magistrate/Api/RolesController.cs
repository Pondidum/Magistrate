using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class RolesController : Controller
	{
		public RolesController(Store store)
			: base(store)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/roles/all").Get(GetAll);
			app.Route("/api/roles").Put(CreateRole);
			app.Route("/api/roles/{role-key}").Get(GetRoleDetails);
			app.Route("/api/role/{role-key}/changeName").Put(ChangeName);
			app.Route("/api/role/{role-key}/changeDescription").Put(ChangeDescription);
			app.Route("/api/role/{role-key}/addPermission/{permission-key}").Put(AddPermission);
			app.Route("/api/role/{role-key}/removePermission/{permission-key}").Put(RemovePermission);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.WriteJson(Store.Roles.AllRoles);
		}

		private async Task CreateRole(IOwinContext context)
		{
			var dto = context.ReadJson<CreateRoleDto>();
			Store.Save(Role.Create(Store.Permissions.ByID, dto.Key, dto.Name, dto.Description));
			await Task.Yield();
		}

		private async Task GetRoleDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role => await context.WriteJson(role));
		}

		private async Task ChangeName(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role =>
			{
				role.ChangeName(ReadBody(context));
				Store.Save(role);

				await Task.Yield();
			});
		}

		private async Task ChangeDescription(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role =>
			{
				role.ChangeDescription(ReadBody(context));
				Store.Save(role);

				await Task.Yield();
			});
		}

		private async Task AddPermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetRole, async role =>
			{
				await NotFoundOrAction(context, GetPermission, async permission =>
				{
					role.AddPermission(permission);
					Store.Save(role);

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
					role.RemovePermission(permission);
					Store.Save(role);

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
