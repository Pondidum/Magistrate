using System.Collections.Generic;
using System.Linq;
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

		[JsonProperty("includes")]
		public Dictionary<string, string> Includes { get; set; }

		[JsonProperty("revokes")]
		public Dictionary<string, string> Revokes { get; set; }

		[JsonProperty("roles")]
		public Dictionary<string, string> Roles { get; set; }  

		public static UserResponse From(User user)
		{
			return new UserResponse
			{
				Key = user.Key,
				Name = user.Name,
				Includes = user.Permissions.Includes.ToDictionary(p => p.Key, p => p.Name),
				Revokes = user.Permissions.Revokes.ToDictionary(p => p.Key, p => p.Name),
				Roles = user.Permissions.Roles.ToDictionary(p => p.Key, p => p.Name)
			};
		}
	}
}
