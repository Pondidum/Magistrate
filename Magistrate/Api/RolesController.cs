﻿using System.Linq;
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
			app.Route("/api/roles/{role-key}/changeName").Put(ChangeName);
			app.Route("/api/roles/{role-key}/changeDescription").Put(ChangeDescription);
			app.Route("/api/roles/{role-key}/permission/{permission-key}").Put(AddPermission);
			app.Route("/api/roles/{role-key}/permission/{permission-key}").Delete(RemovePermission);
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

		private async Task ChangeName(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async key =>
			{
				System.OnRole(key, role => role.ChangeName(context.GetUser(), ReadBody(context)));
				await Task.Yield();
			});
		}

		private async Task ChangeDescription(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async key =>
			{
				System.OnRole(key, role => role.ChangeDescription(context.GetUser(), ReadBody(context)));
				await Task.Yield();
			});
		}

		private async Task AddPermission(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async roleKey =>
			{
				await NotFoundOrAction(context, PermissionKey, async permissionKey =>
				{
					System.OnRole(roleKey, role =>
					{
						role.AddPermission(context.GetUser(), System.LoadPermission(permissionKey));
					});

					await Task.Yield();
				});
			});
		}

		private async Task RemovePermission(IOwinContext context)
		{
			await NotFoundOrAction(context, RoleKey, async roleKey =>
			{
				await NotFoundOrAction(context, PermissionKey, async permissionKey =>
				{
					System.OnRole(roleKey, role =>
					{
						role.RemovePermission(context.GetUser(), System.LoadPermission(permissionKey));
					});

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
