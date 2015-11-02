using System.Linq;
using System.Threading.Tasks;
using Magistrate.Api.Responses;
using Magistrate.Domain;
using Magistrate.Infrastructure;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class UsersController : Controller
	{
		public UsersController(Store store)
			: base(store)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/users/all").Get(GetAll);
			app.Route("/api/users/active").Get(GetActive);
			app.Route("/api/users").Put(CreateUser);
			app.Route("/api/users/{user-key}").Get(GetUserDetails);
			app.Route("/api/users/{user-key}").Delete(DeactivateUser);
			app.Route("/api/users/{user-key}/addPermission/{permission-key}").Put(AddPermission);
			app.Route("/api/users/{user-key}/removePermission/{permission-key}").Put(RemovePermission);
			app.Route("/api/users/{user-key}/addRole/{role-key}").Put(AddRole);
			app.Route("/api/users/{user-key}/removeRole/{role-key}").Put(RemoveRole);
			app.Route("/api/users/{user-key}/can/{permission-key}").Get(CheckPermission);
		}

		private async Task GetAll(IOwinContext context)
		{
			await context.JsonResponse(Store.Users.AllUsers.Select(UserResponse.From));
		}

		private async Task GetActive(IOwinContext context)
		{
			await context.JsonResponse(Store.Users.ActiveUsers.Select(UserResponse.From));
		}

		private async Task CreateUser(IOwinContext context)
		{
			var dto = context.ReadJson<CreateUserDto>();
			var user = User.Create(Store.Permissions.ByID, Store.Roles.ByID, dto.Key, dto.Name);

			var result = Store.Save(user);

			await context.JsonResponse(UserCreateResponse.From(result, user));
		}

		private async Task GetUserDetails(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user => await context.JsonResponse(UserResponse.From(user)));
		}

		private async Task DeactivateUser(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				user.Deactivate();

				await Task.Yield();
			});
		}

		private async Task AddPermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				await NotFoundOrAction(context, GetPermission, async permission =>
				{
					user.AddPermission(permission);
					Store.Save(user);

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
					user.RemovePermission(permission);
					Store.Save(user);

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
					user.AddRole(role);
					Store.Save(user);

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
					user.RemoveRole(role);
					Store.Save(user);

					await Task.Yield();
				});
			});
		}

		private async Task CheckPermission(IOwinContext context)
		{
			await NotFoundOrAction(context, GetUser, async user =>
			{
				await NotFoundOrAction(context, GetPermission, async permission =>
				{
					await context.JsonResponse(user.Permissions.Can(permission));
				});
			});
		}

		private class CreateUserDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
		}
	}
}
