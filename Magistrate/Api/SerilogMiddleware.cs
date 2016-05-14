using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;
using Serilog.Context;

namespace Magistrate.Api
{
	public class SerilogMiddleware : OwinMiddleware
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<SerilogMiddleware>();

		public SerilogMiddleware(OwinMiddleware next) : base(next)
		{
		}

		public override async Task Invoke(IOwinContext context)
		{
			
			var sw = new Stopwatch();
			sw.Start();

			try
			{
				await Next.Invoke(context);
			}
			finally
			{
				sw.Stop();

				var request = context.Request;
				var response = context.Response;

				using (LogContext.PushProperty("sourceAddress", request.RemoteIpAddress))
				{
					Log.Information(
						"Api {httpMethod} {httpCode} to {url} took {elapsed}ms",
						request.Method,
						response.StatusCode,
						request.Uri,
						sw.ElapsedMilliseconds
					);
				}
			}
		}
	}
}
