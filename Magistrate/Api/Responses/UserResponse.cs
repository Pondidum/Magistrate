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
		public IEnumerable<Pair> Includes { get; set; }

		[JsonProperty("revokes")]
		public IEnumerable<Pair> Revokes { get; set; }

		[JsonProperty("roles")]
		public IEnumerable<Pair> Roles { get; set; }

		public static UserResponse From(User user)
		{
			return new UserResponse
			{
				Key = user.Key,
				Name = user.Name,
				Includes = user.Permissions.Includes.Select(p => new Pair { Key = p.Key, Name = p.Name }),
				Revokes = user.Permissions.Revokes.Select(p => new Pair { Key = p.Key, Name = p.Name }),
				Roles = user.Permissions.Roles.Select(p => new Pair { Key = p.Key, Name = p.Name })
			};
		}
	}
}
