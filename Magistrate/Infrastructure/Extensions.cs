using System.Text;
using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Magistrate.Infrastructure
{
	public static class Extensions
	{
		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		/// <summary>
		/// Writes given value as JSON string.
		/// </summary>
		/// <param name="context">The OWIN context.</param>
		/// <param name="value">The value to serialize.</param>
		public static async Task JsonResponse(this IOwinContext context, object value)
		{
			var json = JsonConvert.SerializeObject(value, Settings);
			const string contentType = "application/json";

			context.Response.Headers.Set("Content-Type", contentType);
			context.Response.Headers.Set("Content-Encoding", "utf8");
			context.Response.ContentType = contentType;

			var bytes = Encoding.UTF8.GetBytes(json);
			await context.Response.WriteAsync(bytes);
		}
	}
}