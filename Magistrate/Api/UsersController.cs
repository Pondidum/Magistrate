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
			app.Route("/api/users").Delete(DeleteUser);

			app.Route("/api/users/{user-key}").Get(GetUserDetails);

			app.Route("/api/users/{user-key}/includes").Put(AddInclude);
			app.Route("/api/users/{user-key}/includes").Delete(RemoveInclude);

			app.Route("/api/users/{user-key}/revokes").Put(AddRevoke);
			app.Route("/api/users/{user-key}/revokes").Delete(RemoveRevoke);

			app.Route("/api/users/{user-key}/roles").Put(AddRole);
			app.Route("/api/users/{user-key}/roles").Delete(RemoveRole);

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
			var dto = context.ReadJson<string[]>();
			var currentUser = context.GetUser();

			foreach (var key in dto)
			{
				System.OnUser(key, user => user.Deactivate(currentUser));
			}

			await Task.Yield();
		}

		private async Task AddInclude(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				var dto = context.ReadJson<string[]>();
				var currentUser = context.GetUser();

				foreach (var permissionKey in dto)
				{
					System.OnUser(key, user => user.AddInclude(currentUser, System.LoadPermission(permissionKey)));
				}

				await Task.Yield();
			});
		}

		private async Task RemoveInclude(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				var dto = context.ReadJson<string[]>();
				var currentUser = context.GetUser();

				foreach (var permissionKey in dto)
				{
					System.OnUser(key, user => user.RemoveInclude(currentUser, System.LoadPermission(permissionKey)));
				}

				await Task.Yield();
			});
		}

		private async Task AddRevoke(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				var dto = context.ReadJson<string[]>();
				var currentUser = context.GetUser();

				foreach (var permissionKey in dto)
				{
					System.OnUser(key, user => user.AddRevoke(currentUser, System.LoadPermission(permissionKey)));
				}

				await Task.Yield();
			});
		}

		private async Task RemoveRevoke(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				var dto = context.ReadJson<string[]>();
				var currentUser = context.GetUser();

				foreach (var permissionKey in dto)
				{
					System.OnUser(key, user => user.RemoveRevoke(currentUser, System.LoadPermission(permissionKey)));
				}

				await Task.Yield();
			});
		}

		private async Task AddRole(IOwinContext context)
		{
			await NotFoundOrAction(context, UserKey, async key =>
			{
				var dto = context.ReadJson<string[]>();
				var currentUser = context.GetUser();

				foreach (var roleKey in dto)
				{
					System.OnUser(key, user => user.RemoveRole(currentUser, System.LoadRole(roleKey)));
				}

				await Task.Yield();
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
