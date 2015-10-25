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
		}

		private Permission GetPermission(IOwinContext context)
		{
			var key = context.GetRouteValue<string>("permission-key");
			return _store.Permissions.ByKey(key);
		}

		private string ReadBody(IOwinContext context)
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

		//private static async Task RespondOrNotFound(IOwinContext context, Permission permission)
		//{
		//	if (permission == null)
		//		context.Response.StatusCode = (int)HttpStatusCode.NotFound;
		//	else
		//		await context.WriteJson(permission);
		//}
	}
}
