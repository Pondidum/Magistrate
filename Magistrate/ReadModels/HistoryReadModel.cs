﻿using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Events.RoleEvents;
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
			var roles = new Dictionary<Guid, RoleDto>();

			_entries = new List<string>();

			_projections.Register<PermissionCreatedEvent>(e =>
			{
				permissions[e.ID] = new PermissionDto { Name = e.Name, Description = e.Description };
				_entries.Add($"Permission '{e.Name}' created by {e.Operator.Name}");
			});

			_projections.Register<PermissionNameChangedEvent>(e =>
			{
				_entries.Add($"Permission Name changed from '{permissions[e.AggregateID].Name}' to '{e.NewName}' by {e.Operator.Name}");
			});

			_projections.Register<PermissionDescriptionChangedEvent>(e =>
			{
				_entries.Add($"Permission Description changed from '{permissions[e.AggregateID].Description}' to '{e.NewDescription}' by {e.Operator.Name}");
			});

			_projections.Register<RoleCreatedEvent>(e =>
			{
				roles[e.ID] = new RoleDto { Name = e.Name, Description = e.Description };
				_entries.Add($"Role '{e.Name} created by {e.Operator.Name}");
			});

			_projections.Register<RoleNameChangedEvent>(e =>
			{
				_entries.Add($"Role Name changed from '{roles[e.AggregateID].Name}' to '{e.NewName}' by {e.Operator.Name}");
			});

			_projections.Register<RoleDescriptionChangedEvent>(e =>
			{
				_entries.Add($"Role Description changed from '{roles[e.AggregateID].Description}' to '{e.NewDescription}' by {e.Operator.Name}");
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

		public class RoleDto
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
