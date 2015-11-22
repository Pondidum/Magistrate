using System.Linq;
using System.Threading.Tasks;
using Magistrate.Domain;
using Magistrate.Domain.Services;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class UsersController : Controller
	{
		public UsersController(SystemFacade system)
			: base(system)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/users/all").Get(GetAll);
			app.Route("/api/users").Put(CreateUser);

			app.Route("/api/users/{user-key}").Get(GetUserDetails);
			app.Route("/api/users/{user-key}").Delete(DeleteUser);

			app.Route("/api/users/{user-key}/include/{permission-key}").Put(AddInclude);
			app.Route("/api/users/{user-key}/include/{permission-key}").Delete(RemoveInclude);

			app.Route("/api/users/{user-key}/revoke/{permission-key}").Put(AddRevoke);
			app.Route("/api/users/{user-key}/revoke/{permission-key}").Delete(RemoveRevoke);

			app.Route("/api/users/{user-key}/role/{role-key}").Put(AddRole);
			app.Route("/api/users/{user-key}/role/{role-key}").Delete(RemoveRole);
			
			//app.Route("/api/users/{user-key}/can/{permission-key}").Get(CheckPermission);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.JsonResponse(System.Users);
		}

		private async Task CreateUser(IOwinContext context)
		{
			var dto = context.ReadJson<CreateUserDto>();
			var user = System.CreateUser(context.GetUser(), dto.Key, dto.Name);

			await context.JsonResponse(user);
		}

		private async Task GetUserDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				var user = System.Users.First(u => u.Key == key);
				await context.JsonResponse(user);
			});
		}

		private async Task DeleteUser(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				System.OnPermission(key, user => user.Deactivate(context.GetUser()));

				await Task.Yield();
			});
		}

		private async Task AddInclude(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				await NotFoundOrAction(context, PermissionKey, async permissionKey =>
				{
					System.OnUser(key, user => user.AddInclude(context.GetUser(), System.LoadPermission(permissionKey)));
					await Task.Yield();
				});
			});
		}

		private async Task RemoveInclude(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				await NotFoundOrAction(context, PermissionKey, async permissionKey =>
				{
					System.OnUser(key, user => user.RemoveInclude(context.GetUser(), System.LoadPermission(permissionKey)));
					await Task.Yield();
				});
			});
		}

		private async Task AddRevoke(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				await NotFoundOrAction(context, PermissionKey, async permissionKey =>
				{
					System.OnUser(key, user => user.AddRevoke(context.GetUser(), System.LoadPermission(permissionKey)));
					await Task.Yield();
				});
			});
		}

		private async Task RemoveRevoke(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				await NotFoundOrAction(context, PermissionKey, async permissionKey =>
				{
					System.OnUser(key, user => user.RemoveRevoke(context.GetUser(), System.LoadPermission(permissionKey)));
					await Task.Yield();
				});
			});
		}

		private async Task AddRole(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				await NotFoundOrAction(context, RoleKey, async roleKey =>
				{
					System.OnUser(key, user => user.AddRole(context.GetUser(), System.LoadRole(roleKey)));
					await Task.Yield();
				});
			});
		}

		private async Task RemoveRole(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				await NotFoundOrAction(context, RoleKey, async roleKey =>
				{
					System.OnUser(key, user => user.RemoveRole(context.GetUser(), System.LoadRole(roleKey)));
					await Task.Yield();
				});
			});
		}

		//private async Task CheckPermission(IOwinContext context)
		//{
		//	await NotFoundOrAction(context, GetUser, async key =>
		//	{
		//		await NotFoundOrAction(context, GetPermission, async permission =>
		//		{
		//			await context.JsonResponse(user.Permissions.Can(permission));
		//		});
		//	});
		//}

		private class CreateUserDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
		}
	}
}
