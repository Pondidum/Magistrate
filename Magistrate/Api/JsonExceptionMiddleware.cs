using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Magistrate.Domain;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin.Routing;

namespace Magistrate.Api
{
	internal class JsonExceptionMiddleware : OwinMiddleware
	{
		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		public JsonExceptionMiddleware(OwinMiddleware next)
			: base(next)
		{
			Mapper.CreateMap<Exception, ExceptionResponse>();
			Mapper.CreateMap<RuleViolationException, ExceptionResponse>();
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
				await context.WriteJson(Mapper.Map<ExceptionResponse>(ex), Settings);
			}
		}

		private class ExceptionResponse
		{
			public string Message { get; set; }
			public IEnumerable<string> Violations { get; set; }
		}
	}
}
