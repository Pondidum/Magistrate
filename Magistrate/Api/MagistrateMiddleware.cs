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


			app.Route("/api/users/all").Get(async context =>
			{
				await context.WriteJson(_store.Users.AllUsers);
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
	}
}
