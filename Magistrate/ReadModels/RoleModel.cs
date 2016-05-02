using System;
using System.Collections.Generic;
using Magistrate.Domain;
using Magistrate.Domain.Events.RoleEvents;
using Newtonsoft.Json;

namespace Magistrate.ReadModels
{
	public class RoleModel
	{
		[JsonIgnore]
		public Guid ID { get; set; }

		public RoleKey Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public List<PermissionModel> Permissions { get; private set; } 

		public static RoleModel From(RoleCreatedEvent e)
		{
			return new RoleModel
			{
				ID = e.AggregateID,
				Key = e.Key,
				Name = e.Name,
				Description = e.Description,
				Permissions = new List<PermissionModel>()
			};
		}
	}
}