using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class MagistrateTopware
	{
		private readonly Store _store;

		public MagistrateTopware(Store store)
		{
			_store = store;
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/permissions/all").Get(async context =>
			{
				await context.WriteJson(_store.Permissions.AllPermissions);
			});

			app.Route("/api/permissions").Put(async context =>
			{
				var dto = context.ReadJson<CreatePermissionDto>();

				_store.Save(Permission.Create(dto.Key, dto.Name, dto.Description));

				await Task.Yield();
			});

			app.Route("/api/permissions/{permission-key}").Get(async context =>
			{
				var permission = GetPermission(context);

				await NotFoundOrAction(context, permission, () => context.WriteJson(permission));
			});

			app.Route("/api/permissions/{permission-key}/changeName").Put(async context =>
			{
				var permission = GetPermission(context);

				await NotFoundOrAction(context, permission, async () =>
				{
					permission.ChangeName(ReadBody(context));
					_store.Save(permission);

					await Task.Yield();
				});
			});

			app.Route("/api/permissions/{permission-key}/changeDescription").Put(async context =>
			{
				var permission = GetPermission(context);

				await NotFoundOrAction(context, permission, async () =>
				{
					permission.ChangeDescription(ReadBody(context));
					_store.Save(permission);

					await Task.Yield();
				});
			});

			app.Route("/api/roles/all").Get(async context =>
			{
				await context.WriteJson(_store.Roles.AllRoles);
			});

			app.Route("/api/roles").Put(async context =>
			{
				var dto = context.ReadJson<CreateRoleDto>();

				_store.Save(Role.Create(_store.Permissions.ByID, dto.Key, dto.Name, dto.Description));

				await Task.Yield();
			});

			app.Route("/api/roles/{role-key}").Get(async context =>
			{
				var role = GetRole(context);

				await NotFoundOrAction(context, role, () => context.WriteJson(role));
			});

			app.Route("/api/role/{role-key}/changeName").Put(async context =>
			{
				var role = GetRole(context);

				await NotFoundOrAction(context, role, async () =>
				{
					role.ChangeName(ReadBody(context));
					_store.Save(role);

					await Task.Yield();
				});
			});

			app.Route("/api/role/{role-key}/changeDescription").Put(async context =>
			{
				var role = GetRole(context);

				await NotFoundOrAction(context, role, async () =>
				{
					role.ChangeDescription(ReadBody(context));
					_store.Save(role);

					await Task.Yield();
				});
			});

			app.Route("/api/role/{role-key}/addPermission/{permission-key}").Put(async context =>
			{
				var role = GetRole(context);
				await NotFoundOrAction(context, role, async () =>
				{
					var permission = GetPermission(context);
					await NotFoundOrAction(context, permission, async () =>
					{
						role.AddPermission(permission);
						_store.Save(role);

						await Task.Yield();
					});
				});
			});

			app.Route("/api/role/{role-key}/removePermission/{permission-key}").Put(async context =>
			{
				var role = GetRole(context);
				await NotFoundOrAction(context, role, async () =>
				{
					var permission = GetPermission(context);
					await NotFoundOrAction(context, permission, async () =>
					{
						role.RemovePermission(permission);
						_store.Save(role);

						await Task.Yield();
					});
				});
			});

			app.Route("/api/users/all").Get(async context =>
			{
				await context.WriteJson(_store.Users.AllUsers);
			});

			app.Route("/api/users").Put(async context =>
			{
				var dto = context.ReadJson<CreateUserDto>();

				_store.Save(User.Create(_store.Permissions.ByID, _store.Roles.ByID, dto.Key, dto.Name));

				await Task.Yield();
			});

			app.Route("/api/users/{user-key}").Get(async context =>
			{
				var user = GetUser(context);

				await NotFoundOrAction(context, user, () => context.WriteJson(user));
			});

			app.Route("/api/user/{user-key}/can/{permission-key}").Get(async context =>
			{
				var user = GetUser(context);
				await NotFoundOrAction(context, user, async () =>
				{
					var perm = GetPermission(context);
					await NotFoundOrAction(context, perm, async () =>
					{
						await context.WriteJson(user.Permissions.Can(perm));
					});
				});
			});

		}

		private Permission GetPermission(IOwinContext context)
		{
			var key = context.GetRouteValue<string>("permission-key");
			return _store.Permissions.ByKey(key);
		}

		private Role GetRole(IOwinContext context)
		{
			var key = context.GetRouteValue<string>("role-key");
			return _store.Roles.ByKey(key);
		}

		private User GetUser(IOwinContext context)
		{
			var key = context.GetRouteValue<string>("user-key");
			return _store.Users.ByKey(key);
		}

		private static string ReadBody(IOwinContext context)
		{
			using (var reader = new StreamReader(context.Request.Body))
			{
				return reader.ReadToEnd();
			}
		}

		private static async Task NotFoundOrAction<T>(IOwinContext context, T item, Func<Task> action)
		{
			if (item == null)
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			else
				await action();
		}

		private class CreatePermissionDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		private class CreateRoleDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		private class CreateUserDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
		}

	}

}
