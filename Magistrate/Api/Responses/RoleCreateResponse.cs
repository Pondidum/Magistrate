using System.Collections.Generic;
using AutoMapper;
using Magistrate.Domain;

namespace Magistrate.Api.Responses
{
	public class RoleCreateResponse
	{
		public bool Success { get; set; }
		public IEnumerable<string> Messages { get; set; }
		public RoleResponse Role { get; set; }

		public static RoleCreateResponse From(SaveResult result, Role role)
		{
			var response = new RoleCreateResponse
			{
				Success = result.Success,
				Messages = result.Messages
			};

			if (result.Success)
				response.Role = Mapper.Map<RoleResponse>(role);

			return response;
		}
	}
}
