using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class Controller
	{
		public MagistrateSystem System { get; }

		public Controller(MagistrateSystem system)
		{
			System = system;
		}

		protected static async Task NotFoundOrAction<T>(IOwinContext context, Func<IOwinContext, T> getItem, Func<T, Task> action)
		{
			var item = getItem(context);

			if (item == null)
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			else
				await action(item);
		}

		protected static string ReadBody(IOwinContext context)
		{
			using (var reader = new StreamReader(context.Request.Body))
			{
				return reader.ReadToEnd();
			}
		}

		protected Permission GetPermission(IOwinContext context)
		{
			var key = context.GetRouteValue<string>("permission-key");
			return System.Permissions.ByKey(key);
		}

		protected Role GetRole(IOwinContext context)
		{
			var key = context.GetRouteValue<string>("role-key");
			return System.Roles.ByKey(key);
		}

		protected User GetUser(IOwinContext context)
		{
			var key = context.GetRouteValue<string>("user-key");
			return System.Users.ByKey(key);
		}

	}
}
