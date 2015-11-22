using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Magistrate.Domain.Services;
using Microsoft.Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class Controller
	{
		protected SystemFacade System { get; }

		protected Controller(SystemFacade system)
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

		protected string PermissionKey(IOwinContext context)
		{
			return context.GetRouteValue<string>("permission-key");
		}

		protected string RoleKey(IOwinContext context)
		{
			return context.GetRouteValue<string>("role-key");
		}

		protected string UserKey(IOwinContext context)
		{
			return context.GetRouteValue<string>("user-key");
		}
	}
}
