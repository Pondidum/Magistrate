using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Magistrate.Api.Responses;
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
		private static async Task JsonResponse(this IOwinContext context, object value)
		{
			var json = JsonConvert.SerializeObject(value, Settings);
			const string contentType = "application/json";

			context.Response.Headers.Set("Content-Type", contentType);
			context.Response.Headers.Set("Content-Encoding", "utf8");
			context.Response.ContentType = contentType;

			var bytes = Encoding.UTF8.GetBytes(json);
			await context.Response.WriteAsync(bytes);
		}


		public static async Task JsonResponse(this IOwinContext context, User user)
		{
			await JsonResponse(context, user.Map<UserResponse>());
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<User> users)
		{
			await JsonResponse(context, users.Map<UserResponse>());
		}

		public static async Task JsonResponse(this IOwinContext context, Role role)
		{
			await JsonResponse(context, role.Map<RoleResponse>());
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<Role> roles)
		{
			await JsonResponse(context, roles.Map<RoleResponse>());
		}

		public static async Task JsonResponse(this IOwinContext context, Permission permission)
		{
			await JsonResponse(context, permission.Map<PermissionResponse>());
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<Permission> permissions)
		{
			await JsonResponse(context, permissions.Map<PermissionResponse>());
		}

		public static IEnumerable<TResult> Map<TResult>(this IEnumerable source)
		{
			return source.Cast<object>().Select(Mapper.Map<TResult>);
		}

		public static TResult Map<TResult>(this object source)
		{
			return Mapper.Map<TResult>(source);
		}
	}
}