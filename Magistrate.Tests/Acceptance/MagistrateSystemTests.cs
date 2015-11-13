using System;
using System.Linq;
using Ledger;
using Ledger.Stores;
using Magistrate.Domain;
using Magistrate.Domain.Events;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class MagistrateSystemTests : IDisposable
	{
		private static readonly MagistrateUser CurrentUser = new MagistrateUser();

		private readonly InMemoryEventStore<Guid> _eventStore;
		private readonly MagistrateSystem _system;

		public MagistrateSystemTests()
		{
			_eventStore = new InMemoryEventStore<Guid>();
			_system = new MagistrateSystem(new AggregateStore<Guid>(_eventStore));
		}

		[Fact]
		public void When_adding_and_removing_permission()
		{
			var permission = Permission.Create(CurrentUser, "first", "First Permission", "");

			_system.AddPermission(CurrentUser, permission);
			_system.Permissions.ShouldContain(p => p.Key == "first");

			_system.RemovePermission(CurrentUser, permission);
			_system.Permissions.ShouldBeEmpty();
		}

		[Fact]
		public void When_adding_and_removing_roles()
		{
			var role = Role.Create(CurrentUser, "first", "First Permission", "");

			_system.AddRole(CurrentUser, role);
			_system.Roles.ShouldContain(r => r.Key == "first");

			_system.RemoveRole(CurrentUser, role);
			_system.Roles.ShouldBeEmpty();
		}

		[Fact]
		public void When_adding_and_removing_users()
		{
			var user = User.Create(CurrentUser, "user1", "user");

			_system.AddUser(CurrentUser, user);
			_system.Users.ShouldContain(u => u.Key == "user1");

			_system.RemoveUser(CurrentUser, user);
			_system.Users.ShouldBeEmpty();
		}

		[Fact]
		public void When_removing_a_permission_it_gets_removed_from_roles_too()
		{
			var permission = Permission.Create(CurrentUser, "first-permission", "First Permission", "");
			var role = Role.Create(CurrentUser, "first-role", "First Role", "");
			role.AddPermission(CurrentUser, permission);

			_system.AddPermission(CurrentUser, permission);
			_system.AddRole(CurrentUser, role);

			_system.Permissions.ShouldBe(new[] { permission });
			_system.Roles.ShouldContain(r => r.ID == role.ID);

			_system.RemovePermission(CurrentUser, permission);

			_system.Roles.Single().Permissions.ShouldBeEmpty();
		}

		[Fact]
		public void When_removing_a_permission_it_gets_removed_from_users_too()
		{
			var permission = Permission.Create(CurrentUser, "first-permission", "First Permission", "");
			var user = User.Create(CurrentUser, "first-user", "First User");
			user.AddPermission(CurrentUser, permission);

			_system.AddPermission(CurrentUser, permission);
			_system.AddUser(CurrentUser, user);

			_system.Permissions.ShouldBe(new[] { permission });
			_system.Users.ShouldContain(u => u.ID == user.ID);

			_system.RemovePermission(CurrentUser, permission);

			_system.Users.Single().Includes.ShouldBeEmpty();
		}

		[Fact]
		public void When_removing_a_role_it_gets_removed_from_users()
		{
			var role = Role.Create(CurrentUser, "first-role", "First Role", "");
			var user = User.Create(CurrentUser, "first-user", "First User");
			user.AddRole(CurrentUser, role);

			_system.AddRole(CurrentUser, role);
			_system.AddUser(CurrentUser, user);

			_system.Roles.Single().ID.ShouldBe(role.ID);
			_system.Users.Single().ID.ShouldBe(user.ID);

			_system.RemoveRole(CurrentUser, role);

			_system.Users.Single().Roles.ShouldBeEmpty();
		}

		[Fact]
		public void Adding_two_permissions_with_the_same_key()
		{
			var perm1 = Permission.Create(CurrentUser, "01", "One", "");
			var perm2 = Permission.Create(CurrentUser, "01", "Two", "");

			_system.AddPermission(CurrentUser, perm1);

			var ex = Should.Throw<RuleViolationException>(() => _system.AddPermission(CurrentUser, perm2));

			ex.Violations.Single().ShouldBe("There is already a Permission with the Key '01'");
		}

		[Fact]
		public void Adding_two_roles_with_the_same_key()
		{
			var role1 = Role.Create(CurrentUser, "01", "One", "");
			var role2 = Role.Create(CurrentUser, "01", "Two", "");

			_system.AddRole(CurrentUser, role1);

			var ex = Should.Throw<RuleViolationException>(() => _system.AddRole(CurrentUser, role2));

			ex.Violations.Single().ShouldBe("There is already a Role with the Key '01'");
		}

		[Fact]
		public void Adding_two_users_with_the_same_key()
		{
			var user1 = User.Create(CurrentUser, "01", "One");
			var user2 = User.Create(CurrentUser, "01", "Two");

			_system.AddUser(CurrentUser, user1);

			var ex = Should.Throw<RuleViolationException>(() => _system.AddUser(CurrentUser, user2));

			ex.Violations.Single().ShouldBe("There is already a User with the Key '01'");
		}

		public void Dispose()
		{
			_system.Save();

			_eventStore
				.AllEvents
				.Cast<UserLoggedEvent>()
				.ShouldAllBe(e => e.User != null);
		}
	}
}
