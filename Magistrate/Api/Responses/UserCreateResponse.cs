using System.Collections.Generic;
using AutoMapper;
using Magistrate.Domain;

namespace Magistrate.Api.Responses
{
	public class UserCreateResponse
	{
		public bool Success { get; set; }
		public IEnumerable<string> Messages { get; set; }
		public UserResponse User { get; set; }

		public static UserCreateResponse From(SaveResult result, User user)
		{
			var response = new UserCreateResponse
			{
				Success = result.Success,
				Messages = result.Messages
			};

			if (result.Success)
				response.User = Mapper.Map<UserResponse>(user);

			return response;
		}
	}
}
