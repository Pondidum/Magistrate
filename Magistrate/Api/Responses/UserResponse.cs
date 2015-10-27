using Magistrate.Domain;
using Newtonsoft.Json;

namespace Magistrate.Api.Responses
{
	public class UserResponse
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		public static UserResponse From(User arg)
		{
			return new UserResponse
			{
				Key = arg.Key,
				Name = arg.Name
			};
		}
	}
}
