using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin.Routing;
using Serilog;

namespace Magistrate.Api
{
	public class ExceptionHandlerMiddleware : OwinMiddleware
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<ExceptionHandlerMiddleware>();

		private readonly JsonSerializerSettings _jsonSettings;
		private readonly Dictionary<Type, Func<IOwinContext, Exception, Task>> _handlers;

		public ExceptionHandlerMiddleware(OwinMiddleware next, JsonSerializerSettings jsonSettings)
			: base(next)
		{
			_jsonSettings = jsonSettings;
			_handlers = new Dictionary<Type, Func<IOwinContext, Exception, Task>>();

			Register<DuplicatePermissionException>(async (context, ex) =>
			{
				context.Response.StatusCode = 409; // 409:Conflict
				await context.WriteJson(new { Message = ex.Message }, _jsonSettings);
			});

			Register<DuplicateRoleException>(async (context, ex) =>
			{
				context.Response.StatusCode = 409; // 409:Conflict
				await context.WriteJson(new { Message = ex.Message }, _jsonSettings);
			});

			Register<DuplicateUserException>(async (context, ex) =>
			{
				context.Response.StatusCode = 409; // 409:Conflict
				await context.WriteJson(new { Message = ex.Message }, _jsonSettings);
			});
		}

		private void Register<TException>(Func<IOwinContext, TException, Task> apply)
			where TException : Exception
		{
			_handlers[typeof(TException)] = (context, ex) => apply(context, (TException)ex);

		}
		public override async Task Invoke(IOwinContext context)
		{
			try
			{
				await Next.Invoke(context);
			}
			catch (Exception ex)
			{
				Log.Error(ex, ex.ToString());

				Func<IOwinContext, Exception, Task> handler;

				if (_handlers.TryGetValue(ex.GetType(), out handler))
				{
					await handler(context, ex);
				}
				else
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					await context.WriteJson(ex, _jsonSettings);
				}
			}
		}
	}
}
