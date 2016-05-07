using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Infrastructure;

namespace Magistrate.ReadModels
{
	public class HistoryReadModel
	{
		public IEnumerable<string> Entries => _entries;
		 
		private readonly Projector _projections;
		private readonly List<string> _entries;

		public HistoryReadModel()
		{
			_projections = new Projector();

			var permissions = new Dictionary<Guid, PermissionDto>();

			_entries = new List<string>();

			_projections.Register<PermissionCreatedEvent>(e =>
			{
				permissions[e.ID] = new PermissionDto { Name = e.Name, Description = e.Description };
				_entries.Add($"Permission '{e.Name}' created by ${e.Operator.Name}");
			});

			_projections.Register<PermissionNameChangedEvent>(e =>
			{
				_entries.Add($"Permission Name changed from '{permissions[e.AggregateID].Name}' to '{e.NewName}' by {e.Operator.Name}");
			});

			_projections.Register<PermissionDescriptionChangedEvent>(e =>
			{
				_entries.Add($"Permission Description changed from '{permissions[e.AggregateID].Description}' to '{e.NewDescription}' by {e.Operator.Name}");
			});
		}

		public void Project(DomainEvent<Guid> e)
		{
			_projections.Apply(e);
		}


		public class PermissionDto
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
