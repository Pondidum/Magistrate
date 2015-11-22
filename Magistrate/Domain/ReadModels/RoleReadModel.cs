using System;
using System.Collections.Generic;
using Magistrate.Domain.Events.RoleEvents;

namespace Magistrate.Domain.ReadModels
{
	public class RoleReadModel : IKeyed, IIdentity
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public HashSet<PermissionReadModel> Permissions { get; }

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
	}
}