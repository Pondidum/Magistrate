using System;
using Magistrate.Domain;
using Magistrate.Domain.Events.PermissionEvents;
using Newtonsoft.Json;

namespace Magistrate.ReadModels
{
	public class PermissionModel
	{
		[JsonIgnore]
		public Guid ID { get; set; }

		public PermissionKey Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public static PermissionModel From(PermissionCreatedEvent e)
		{
			return new PermissionModel
			{
				ID = e.AggregateID,
				Key = e.Key,
				Name = e.Name,
				Description = e.Description
			};
		}
	}
}