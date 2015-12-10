using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain.Events.RoleEvents;

namespace Magistrate.Domain.ReadModels
{
	public class RoleReadModel : IKeyed, IIdentity
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public HashSet<PermissionReadModel> Permissions { get; set; }

		public RoleReadModel()
		{
			Permissions = new HashSet<PermissionReadModel>();
		}

		public static RoleReadModel From(RoleCreatedEvent e)
		{
			return new RoleReadModel
			{
				ID = e.ID,
				Key = e.Key,
				Name = e.Name,
				Description = e.Description
			};
		}

		public static RoleReadModel From(Role r, Dictionary<Guid, PermissionReadModel> permissions)
		{
			var model = new RoleReadModel
			{
				ID = r.ID,
				Key = r.Key,
				Name = r.Name,
				Description = r.Description,
				Permissions = r.Permissions.Join(permissions, g => g, rm => rm.Key, (g, rm) => rm.Value).ToHashSet()
			};

			return model;
		}
	}
}