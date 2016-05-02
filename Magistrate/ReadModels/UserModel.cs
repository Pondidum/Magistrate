using System;
using System.Collections.Generic;
using Magistrate.Domain;
using Magistrate.Domain.Events.UserEvents;
using Newtonsoft.Json;

namespace Magistrate.ReadModels
{
	public class UserModel
	{
		[JsonIgnore]
		public Guid ID { get; set; }

		public UserKey Key { get; set; }
		public string Name { get; set; }

		public List<PermissionModel> Includes { get; private set; }
		public List<PermissionModel> Revokes { get; private set; }
		public List<RoleModel> Roles { get; private set; }

		public static UserModel From(UserCreatedEvent e)
		{
			return new UserModel
			{
				ID = e.AggregateID,
				Key = e.Key,
				Name = e.Name,
				Includes = new List<PermissionModel>(),
				Revokes = new List<PermissionModel>(),
				Roles = new List<RoleModel>()
			};

		}
	}
}