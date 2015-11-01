using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace Magistrate.Infrastructure
{
	public static class Extensions
	{
		/// <summary>
		/// Writes given value as JSON string.
		/// </summary>
		/// <param name="context">The OWIN context.</param>
		/// <param name="value">The value to serialize.</param>
		public static async Task JsonResponse(this IOwinContext context, object value)
		{
			var json = JsonConvert.SerializeObject(value);
			const string contentType = "application/json";

			context.Response.Headers.Set("Content-Type", contentType);
			context.Response.Headers.Set("Content-Encoding", "utf8");
			context.Response.ContentType = contentType;

			var bytes = Encoding.UTF8.GetBytes(json);
			await context.Response.WriteAsync(bytes);
		}
	}
}