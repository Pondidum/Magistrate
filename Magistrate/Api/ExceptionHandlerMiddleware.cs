using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin.Routing;

namespace Magistrate.Api
{
	public class ExceptionHandlerMiddleware : OwinMiddleware
	{
		private readonly JsonSerializerSettings _jsonSettings;

		public ExceptionHandlerMiddleware(OwinMiddleware next, JsonSerializerSettings jsonSettings)
			: base(next)
		{
			_jsonSettings = jsonSettings;
		}

		public override async Task Invoke(IOwinContext context)
		{
			try
			{
				await Next.Invoke(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.WriteJson(ex, _jsonSettings);
			}
		}
	}
}