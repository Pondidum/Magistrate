using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain.Events.UserEvents;

namespace Magistrate.Domain.ReadModels
{
	public class UserReadModel : IKeyed, IIdentity
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }

		public HashSet<PermissionReadModel> Includes { get; set; }
		public HashSet<PermissionReadModel> Revokes { get; set; }
		public HashSet<RoleReadModel> Roles { get; set; }

		public UserReadModel()
		{
			Includes = new HashSet<PermissionReadModel>();
			Revokes = new HashSet<PermissionReadModel>();
			Roles = new HashSet<RoleReadModel>();
		}

		public static UserReadModel From(UserCreatedEvent e)
		{
			return new UserReadModel
			{
				ID = e.ID,
				Key = e.Key,
				Name = e.Name,
				IsActive = true
			};
		}
	}
}