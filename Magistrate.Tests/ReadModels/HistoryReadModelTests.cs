using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Magistrate.Domain;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Events.UserEvents;
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

		public static IEnumerable<object[]> AllEvents
		{
			get
			{
				var eventBase = typeof(DomainEvent<Guid>);

				return typeof(HistoryReadModel)
					.Assembly
					.GetTypes()
					.Where(t => t.IsAbstract == false)
					.Where(t => eventBase.IsAssignableFrom(t))
					.Select(t => new[] { t });
			}
		}

		[Theory]
		[MemberData("AllEvents")]
		public void It_should_subscribe_to_all_events(Type eventType)
		{
			_model.HasRegistration(eventType).ShouldBe(true, $"{eventType.Name} is not registered.");
		}

		[Fact]
		public void When_a_permission_is_created()
		{
			_model.Project(new PermissionCreatedEvent(_user1, Guid.NewGuid(), new PermissionKey("p1"), "First Perm", "Does something"));

			_model.Entries.Single().Description.ShouldBe($"Permission 'First Perm' created by {_user1.Name}");
		}

		[Fact]
		public void When_a_permission_is_renamed()
		{
			var id = Guid.NewGuid();
			_model.Project(new PermissionCreatedEvent(_user1, id, new PermissionKey("p1"), "First Perm", "Does something"));
			_model.Project(new PermissionNameChangedEvent(_user2, "First Permission") { AggregateID = id });

			var entries = _model.Entries.ToList();

			entries[0].Description.ShouldBe($"Permission 'First Perm' created by {_user1.Name}");
			entries[1].Description.ShouldBe($"Permission Name changed from 'First Perm' to 'First Permission' by {_user2.Name}");
		}

		[Fact]
		public void When_a_permissions_description_is_changed()
		{
			var id = Guid.NewGuid();
			_model.Project(new PermissionCreatedEvent(_user1, id, new PermissionKey("p1"), "First Perm", "Does something"));
			_model.Project(new PermissionDescriptionChangedEvent(_user2, "Does things") { AggregateID = id });

			var entries = _model.Entries.ToList();

			entries[0].Description.ShouldBe($"Permission 'First Perm' created by {_user1.Name}");
			entries[1].Description.ShouldBe($"Permission Description changed from 'Does something' to 'Does things' by {_user2.Name}");
		}

		[Fact]
		public void When_a_role_is_created()
		{
			_model.Project(new RoleCreatedEvent(_user1, Guid.NewGuid(), new RoleKey("p1"), "First Role", "Does something"));

			_model.Entries.Single().Description.ShouldBe($"Role 'First Role' created by {_user1.Name}");
		}

		[Fact]
		public void When_a_role_is_renamed()
		{
			var id = Guid.NewGuid();
			_model.Project(new RoleCreatedEvent(_user1, id, new RoleKey("p1"), "First Role", "Does something"));
			_model.Project(new RoleNameChangedEvent(_user2, "First Role") { AggregateID = id });

			var entries = _model.Entries.ToList();

			entries[0].Description.ShouldBe($"Role 'First Role' created by {_user1.Name}");
			entries[1].Description.ShouldBe($"Role Name changed from 'First Role' to 'First Role' by {_user2.Name}");
		}

		[Fact]
		public void When_a_roles_description_is_changed()
		{
			var id = Guid.NewGuid();
			_model.Project(new RoleCreatedEvent(_user1, id, new RoleKey("p1"), "First Role", "Does something"));
			_model.Project(new RoleDescriptionChangedEvent(_user2, "Does things") { AggregateID = id });

			var entries = _model.Entries.ToList();

			entries[0].Description.ShouldBe($"Role 'First Role' created by {_user1.Name}");
			entries[1].Description.ShouldBe($"Role Description changed from 'Does something' to 'Does things' by {_user2.Name}");
		}

		[Fact]
		public void When_a_user_is_created()
		{
			_model.Project(new UserCreatedEvent(_user1, Guid.NewGuid(), new UserKey("p1"), "First User"));

			_model.Entries.Single().Description.ShouldBe($"User 'First User' created by {_user1.Name}");
		}

		[Fact]
		public void When_a_user_is_renamed()
		{
			var id = Guid.NewGuid();
			_model.Project(new UserCreatedEvent(_user1, id, new UserKey("p1"), "First User"));
			_model.Project(new UserNameChangedEvent(_user2, "First User") { AggregateID = id });

			var entries = _model.Entries.ToList();

			entries[0].Description.ShouldBe($"User 'First User' created by {_user1.Name}");
			entries[1].Description.ShouldBe($"User Name changed from 'First User' to 'First User' by {_user2.Name}");
		}

		[Fact]
		public void When_a_permission_is_added_to_a_role()
		{
			var permID = Guid.NewGuid();
			var roleID = Guid.NewGuid();

			_model.Project(new PermissionCreatedEvent(_user1, permID, new PermissionKey("1"), "Perm One", "1 Desc"));
			_model.Project(new RoleCreatedEvent(_user1, roleID, new RoleKey("1"), "Role One", "1 Desc"));
			_model.Project(new PermissionAddedToRoleEvent(_user1, permID) { AggregateID = roleID });

			_model.Entries.Last().Description.ShouldBe(
				"Permission 'Perm One' added to Role 'Role One' by Andy Dote'"
			);
		}

		[Fact]
		public void When_a_permission_is_removed_from_a_role()
		{
			var permID = Guid.NewGuid();
			var roleID = Guid.NewGuid();

			_model.Project(new PermissionCreatedEvent(_user1, permID, new PermissionKey("1"), "Perm One", "1 Desc"));
			_model.Project(new RoleCreatedEvent(_user1, roleID, new RoleKey("1"), "Role One", "1 Desc"));
			_model.Project(new PermissionRemovedFromRoleEvent(_user1, permID) { AggregateID = roleID });

			_model.Entries.Last().Description.ShouldBe(
				"Permission 'Perm One' removed from Role 'Role One' by Andy Dote'"
			);
		}
	}
}
