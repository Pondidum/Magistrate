using System;
using System.Linq;
using Magistrate.Domain;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.ReadModels;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.ReadModels
{
	public class HistoryReadModelTests
	{
		private readonly HistoryReadModel _model;
		private Operator _user1;
		private Operator _user2;

		public HistoryReadModelTests()
		{
			_model = new HistoryReadModel();

			_user1 = new Operator { Name = "Andy Dote" };
			_user2 = new Operator { Name = "John Wick" };
		}

		[Fact]
		public void When_a_permission_is_created()
		{
			_model.Project(new PermissionCreatedEvent(_user1, Guid.NewGuid(), new PermissionKey("p1"), "First Perm", "Does something"));

			_model.Entries.Single().ShouldBe($"Permission 'First Perm' created by {_user1.Name}");
		}

		[Fact]
		public void When_a_permission_is_renamed()
		{
			var id = Guid.NewGuid();
			_model.Project(new PermissionCreatedEvent(_user1, id, new PermissionKey("p1"), "First Perm", "Does something"));
			_model.Project(new PermissionNameChangedEvent(_user2, "First Permission") { AggregateID = id });

			var entries = _model.Entries.ToList();

			entries[0].ShouldBe($"Permission 'First Perm' created by {_user1.Name}");
			entries[1].ShouldBe($"Permission Name changed from 'First Perm' to 'First Permission' by {_user2.Name}");
		}

		[Fact]
		public void When_a_permissions_description_is_changed()
		{
			var id = Guid.NewGuid();
			_model.Project(new PermissionCreatedEvent(_user1, id, new PermissionKey("p1"), "First Perm", "Does something"));
			_model.Project(new PermissionDescriptionChangedEvent(_user2, "Does things") { AggregateID = id });

			var entries = _model.Entries.ToList();

			entries[0].ShouldBe($"Permission 'First Perm' created by {_user1.Name}");
			entries[1].ShouldBe($"Permission Description changed from 'Does something' to 'Does things' by {_user2.Name}");
		}
	}
}
