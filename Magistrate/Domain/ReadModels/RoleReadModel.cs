using System;
using System.Collections.Generic;
using Magistrate.Domain.Events.RoleEvents;
using Newtonsoft.Json;

namespace Magistrate.Domain.ReadModels
{
	public class RoleReadModel : IKeyed, IIdentity
	{
		[JsonIgnore]
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
	}
}
