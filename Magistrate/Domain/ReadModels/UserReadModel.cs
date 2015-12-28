using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain.Events.UserEvents;
using Newtonsoft.Json;

namespace Magistrate.Domain.ReadModels
{
	public class UserReadModel : IKeyed<UserKey>, IIdentity
	{
		[JsonIgnore]
		public Guid ID { get; set; }
		public UserKey Key { get; set; }
		public string Name { get; set; }

		public HashSet<PermissionReadModel> Includes { get; }
		public HashSet<PermissionReadModel> Revokes { get; }
		public HashSet<RoleReadModel> Roles { get; }

		public UserReadModel()
		{
			Includes = new HashSet<PermissionReadModel>();
			Revokes = new HashSet<PermissionReadModel>();
			Roles = new HashSet<RoleReadModel>();
		}

		public bool Can(PermissionKey permissionKey)
		{
			if (Revokes.Any(revoke => revoke.Key == permissionKey))
				return false;

			if (Includes.Any(include => include.Key == permissionKey))
				return true;

			if (Roles.Any(role => role.Permissions.Any(perm => perm.Key == permissionKey)))
				return true;

			return false;
		}

		public static UserReadModel From(UserCreatedEvent e)
		{
			return new UserReadModel
			{
				ID = e.ID,
				Key = e.Key,
				Name = e.Name,
			};
		}
	}
}
