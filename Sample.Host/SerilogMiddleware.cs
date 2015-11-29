using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;
using Serilog.Context;

namespace Sample.Host
{
	public class SerilogMiddleware : OwinMiddleware
	{
		public SerilogMiddleware(OwinMiddleware next) : base(next)
		{
		}

		public override async Task Invoke(IOwinContext context)
		{
			var log = Log.ForContext<SerilogMiddleware>();
			var sw = new Stopwatch();
			sw.Start();

			await Next.Invoke(context);

			sw.Stop();

			var request = context.Request;
			var response = context.Response;

			using (LogContext.PushProperty("sourceAddress", request.RemoteIpAddress))
			{
				log.Information(
					"Api {httpMethod} {httpCode} to {url} took {elapsed}ms",
					request.Method,
					response.StatusCode + " " + Enum.Parse(typeof(HttpStatusCode), Convert.ToString(response.StatusCode)),
					request.Uri,
					sw.ElapsedMilliseconds
				);
			}
		}
	}
}
