using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;

namespace Magistrate.Api.Responses
{
	public class UserResponse
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }

		public IEnumerable<Pair> Permissions { get; set; }
		public IEnumerable<Pair> Includes { get; set; }
		public IEnumerable<Pair> Revokes { get; set; }
		public IEnumerable<Pair> Roles { get; set; }

		public static UserResponse From(User user)
		{
			return new UserResponse
			{
				Key = user.Key,
				Name = user.Name,
				Permissions = user.Permissions.All.Select(p => new Pair { Key = p.Key, Name = p.Name }),
				Includes = user.Permissions.Includes.Select(p => new Pair { Key = p.Key, Name = p.Name }),
				Revokes = user.Permissions.Revokes.Select(p => new Pair { Key = p.Key, Name = p.Name }),
				Roles = user.Permissions.Roles.Select(p => new Pair { Key = p.Key, Name = p.Name })
			};
		}
	}
}
