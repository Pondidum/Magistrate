using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Magistrate.Api.Responses;
using Magistrate.Domain.ReadModels;
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

		[DebuggerStepThrough]
		public static MagistrateUser GetUser(this IOwinContext context)
		{
			return context.Get<MagistrateUser>("user");
		}


		public static async Task JsonResponse(this IOwinContext context, object obj)
		{
			await context.WriteJson(obj, Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, PermissionReadModel permission)
		{
			await context.WriteJson(permission.Map<PermissionResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<PermissionReadModel> permissions)
		{
			await context.WriteJson(permissions.Map<PermissionResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, RoleReadModel role)
		{
			await context.WriteJson(role.Map<RoleResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<RoleReadModel> roles)
		{
			await context.WriteJson(roles.Map<RoleResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, UserReadModel user)
		{
			await context.WriteJson(user.Map<UserResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<UserReadModel> users)
		{
			await context.WriteJson(users.Map<UserResponse>(), Settings);
		}

		public static async Task JsonResponse(this IOwinContext context, IEnumerable<HistoryReadModel> users)
		{
			await context.WriteJson(users.Map<HistoryResponse>(), Settings);
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
