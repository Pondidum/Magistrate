using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class UsersController : Controller
	{
		public UsersController(MagistrateSystem system)
			: base(system)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/users/all").Get(GetAll);
			app.Route("/api/users").Put(CreateUser);
			app.Route("/api/users/{user-key}").Get(GetUserDetails);
			app.Route("/api/users/{user-key}").Delete(DeactivateUser);
			app.Route("/api/users/{user-key}/permission/{permission-key}").Put(AddPermission);
			app.Route("/api/users/{user-key}/permission/{permission-key}").Delete(RemovePermission);
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
			var user = User.Create(context.GetUser(), dto.Key, dto.Name);

			System.AddUser(context.GetUser(), user);

			await context.JsonResponse(user);
		}

		private async Task GetUserDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user => await context.JsonResponse(user));
		}

		private async Task DeactivateUser(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				System.RemoveUser(context.GetUser(), user);

				await Task.Yield();
			});
		}

		private async Task AddPermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				await NotFoundOrAction(context, GetPermission, async permission =>
				{
					user.AddPermission(context.GetUser(), permission);

					await Task.Yield();
				});
			});
		}

		private async Task RemovePermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				await NotFoundOrAction(context, GetPermission, async permission =>
				{
					user.RemovePermission(context.GetUser(), permission);

					await Task.Yield();
				});
			});
		}

		private async Task AddRole(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				await NotFoundOrAction(context, GetRole, async role =>
				{
					user.AddRole(context.GetUser(), role);

					await Task.Yield();
				});
			});
		}

		private async Task RemoveRole(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				await NotFoundOrAction(context, GetRole, async role =>
				{
					user.RemoveRole(context.GetUser(), role);

					await Task.Yield();
				});
			});
		}

		//private async Task CheckPermission(IOwinContext context)
		//{
		//	await NotFoundOrAction(context, GetUser, async user =>
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
