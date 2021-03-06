﻿using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Infrastructure;

namespace Magistrate.ReadModels
{
	public class HistoryReadModel
	{
		public IEnumerable<HistoryEntry> Entries => _entries;

		private readonly Projector _projections;
		private readonly List<HistoryEntry> _entries;

		public HistoryReadModel()
		{
			_projections = new Projector();

			var permissions = new Dictionary<Guid, PermissionDto>();
			var roles = new Dictionary<Guid, RoleDto>();
			var users = new Dictionary<Guid, UserDto>();

			_entries = new List<HistoryEntry>();

			_projections.Register<PermissionCreatedEvent>(e =>
			{
				permissions[e.ID] = new PermissionDto { Name = e.Name, Description = e.Description };
				_entries.Add(new HistoryEntry(e, $"Permission '{e.Name}' created by {e.Operator.Name}"));
			});

			_projections.Register<PermissionDeletedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission '{permissions[e.AggregateID].Name} deleted by {e.Operator.Name}"));
			});

			_projections.Register<PermissionNameChangedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission Name changed from '{permissions[e.AggregateID].Name}' to '{e.NewName}' by {e.Operator.Name}"));
			});

			_projections.Register<PermissionDescriptionChangedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission Description changed from '{permissions[e.AggregateID].Description}' to '{e.NewDescription}' by {e.Operator.Name}"));
			});

			_projections.Register<RoleCreatedEvent>(e =>
			{
				roles[e.ID] = new RoleDto { Name = e.Name, Description = e.Description };
				_entries.Add(new HistoryEntry(e, $"Role '{e.Name}' created by {e.Operator.Name}"));
			});

			_projections.Register<RoleDeletedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Role '{roles[e.AggregateID].Name}' deleted by {e.Operator.Name}"));
			});

			_projections.Register<RoleNameChangedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Role Name changed from '{roles[e.AggregateID].Name}' to '{e.NewName}' by {e.Operator.Name}"));
			});

			_projections.Register<RoleDescriptionChangedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Role Description changed from '{roles[e.AggregateID].Description}' to '{e.NewDescription}' by {e.Operator.Name}"));
			});

			_projections.Register<UserCreatedEvent>(e =>
			{
				users[e.ID] = new UserDto { Name = e.Name };
				_entries.Add(new HistoryEntry(e, $"User '{e.Name}' created by {e.Operator.Name}"));
			});

			_projections.Register<UserDeletedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"User '{users[e.AggregateID].Name}' deleted by {e.Operator.Name}"));
			});

			_projections.Register<UserNameChangedEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"User Name changed from '{users[e.AggregateID].Name}' to '{e.NewName}' by {e.Operator.Name}"));
			});

			_projections.Register<PermissionAddedToRoleEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission '{permissions[e.PermissionID].Name}' added to Role '{roles[e.AggregateID].Name}' by {e.Operator.Name}'"));
			});

			_projections.Register<PermissionRemovedFromRoleEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission '{permissions[e.PermissionID].Name}' removed from Role '{roles[e.AggregateID].Name}' by {e.Operator.Name}'"));
			});

			_projections.Register<IncludeAddedToUserEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission '{permissions[e.PermissionID].Name}' added to '{users[e.AggregateID].Name}''s Includes by {e.Operator.Name}'"));
			});

			_projections.Register<IncludeRemovedFromUserEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission '{permissions[e.PermissionID].Name}' removed from '{users[e.AggregateID].Name}''s Includes by {e.Operator.Name}'"));
			});

			_projections.Register<RevokeAddedToUserEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission '{permissions[e.PermissionID].Name}' added to '{users[e.AggregateID].Name}''s Revokes by {e.Operator.Name}'"));
			});

			_projections.Register<RevokeRemovedFromUserEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Permission '{permissions[e.PermissionID].Name}' removed from '{users[e.AggregateID].Name}''s Revokes by {e.Operator.Name}'"));
			});

			_projections.Register<RoleAddedToUserEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Role '{roles[e.RoleID].Name}' added to '{users[e.AggregateID].Name}' by {e.Operator.Name}'"));
			});

			_projections.Register<RoleRemovedFromUserEvent>(e =>
			{
				_entries.Add(new HistoryEntry(e, $"Role '{roles[e.RoleID].Name}' removed from '{users[e.AggregateID].Name}' by {e.Operator.Name}'"));
			});
		}

		public bool HasRegistration(Type eventType)
		{
			return _projections.HasRegistration(eventType);
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

		public class UserDto
		{
			public string Name { get; set; }
		}
	}
}
