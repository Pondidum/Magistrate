using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Magistrate.Api.Responses;
using Magistrate.Domain;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin.Routing;

namespace Magistrate.Api
{
	public static class Extensions
	{
		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};


		public static T ByKey<T>(this IEnumerable<T> collection, string key)
			where T : IKeyed
		{
			return collection.FirstOrDefault(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
		}

		public static MagistrateUser GetUser(this IOwinContext context)
		{
			return context.Get<MagistrateUser>("user");
		}


		public static async Task JsonResponse(this IOwinContext context, Exception ex)
		{
			await context.WriteJson(ex, Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, Permission permission)
		{
			await context.WriteJson(permission.Map<PermissionResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<Permission> permissions)
		{
			await context.WriteJson(permissions.Map<PermissionResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, Role role)
		{
			await context.WriteJson(role.Map<RoleResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<Role> roles)
		{
			await context.WriteJson(roles.Map<RoleResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, User user)
		{
			await context.WriteJson(user.Map<UserResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<User> users)
		{
			await context.WriteJson(users.Map<UserResponse>(), Settings);
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
