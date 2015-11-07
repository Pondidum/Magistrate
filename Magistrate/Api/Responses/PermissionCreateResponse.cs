using System.Collections.Generic;
using AutoMapper;
using Magistrate.Domain;

namespace Magistrate.Api.Responses
{
	public class PermissionCreateResponse
	{
		public bool Success { get; set; }
		public IEnumerable<string> Messages { get; set; }
		public PermissionResponse Permission { get; set; }

		public static PermissionCreateResponse From(SaveResult result, Permission permission)
		{
			var response = new PermissionCreateResponse
			{
				Success = result.Success,
				Messages = result.Messages
			};

			if (result.Success)
				response.Permission = Mapper.Map<PermissionResponse>(permission);

			return response;
		}
	}
}
