using System.Collections.Generic;
using Magistrate.Domain;
using Newtonsoft.Json;

namespace Magistrate.Api.Responses
{
	public class UserCreateResponse
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("messages")]
		public IEnumerable<string> Messages { get; set; }

		[JsonProperty("user")]
		public UserResponse User { get; set; }

		public static UserCreateResponse From(SaveResult result, User user)
		{
			var response = new UserCreateResponse
			{
				Success = result.Success,
				Messages = result.Messages
			};

			if (result.Success)
				response.User = UserResponse.From(user);

			return response;
		}
	}
}
