using System;
using Magistrate.Domain.Events.PermissionEvents;

namespace Magistrate.Domain.ReadModels
{
	public class PermissionReadModel : IKeyed, IIdentity
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public static PermissionReadModel From(PermissionCreatedEvent e)
		{
			return new PermissionReadModel
			{
				ID = e.ID,
				Key = e.Key,
				Name = e.Name,
				Description = e.Description
			};
		}
	}
}
