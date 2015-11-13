using System;
using System.Linq;
using Ledger;
using Ledger.Stores;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class MagistrateSystemTests
	{
		private static readonly MagistrateUser CurrentUser = new MagistrateUser();
		private readonly MagistrateSystem _system;

		public MagistrateSystemTests()
		{
			var store = new AggregateStore<Guid>(new InMemoryEventStore<Guid>());
			_system = new MagistrateSystem(store);
		}

		[Fact]
		public void When_adding_and_removing_permission()
		{
			var store = new AggregateStore<Guid>(new InMemoryEventStore<Guid>());
			var system = new MagistrateSystem(store);

			var permission = Permission.Create(CurrentUser, "first", "First Permission", "");

			system.AddPermission(permission);
			system.Permissions.ShouldContain(p => p.Key == "first");

			system.RemovePermission(permission);
			system.Permissions.ShouldBeEmpty();
		}

		[Fact]
		public void When_adding_and_removing_roles()
		{
			var role = Role.Create(CurrentUser, "first", "First Permission", "");

			_system.AddRole(role);
			_system.Roles.ShouldContain(r => r.Key == "first");

			_system.RemoveRole(role);
			_system.Roles.ShouldBeEmpty();
		}

		[Fact]
		public void When_adding_and_removing_users()
		{
			var user = User.Create(CurrentUser, "user1", "user");

			_system.AddUser(user);
			_system.Users.ShouldContain(u => u.Key == "user1");

			_system.RemoveUser(user);
			_system.Users.ShouldBeEmpty();
		}

		[Fact]
		public void When_removing_a_permission_it_gets_removed_from_roles_too()
		{
			var permission = Permission.Create(CurrentUser, "first-permission", "First Permission", "");
			var role = Role.Create(CurrentUser, "first-role", "First Role", "");
			role.AddPermission(CurrentUser, permission);

			_system.AddPermission(permission);
			_system.AddRole(role);

			_system.Permissions.ShouldBe(new[] { permission });
			_system.Roles.ShouldContain(r => r.ID == role.ID);

			_system.RemovePermission(permission);

			_system.Roles.Single().Permissions.ShouldBeEmpty();
		}

		[Fact]
		public void When_removing_a_permission_it_gets_removed_from_users_too()
		{
			var permission = Permission.Create(CurrentUser, "first-permission", "First Permission", "");
			var user = User.Create(CurrentUser, "first-user", "First User");
			user.AddPermission(CurrentUser, permission);

			_system.AddPermission(permission);
			_system.AddUser(user);

			_system.Permissions.ShouldBe(new[] { permission });
			_system.Users.ShouldContain(u => u.ID == user.ID);

			_system.RemovePermission(permission);

			_system.Users.Single().Includes.ShouldBeEmpty();
		}

		[Fact]
		public void When_removing_a_role_it_gets_removed_from_users()
		{
			var role = Role.Create(CurrentUser, "first-role", "First Role", "");
			var user = User.Create(CurrentUser, "first-user", "First User");
			user.AddRole(CurrentUser, role);

			_system.AddRole(role);
			_system.AddUser(user);

			_system.Roles.Single().ID.ShouldBe(role.ID);
			_system.Users.Single().ID.ShouldBe(user.ID);

			_system.RemoveRole(role);

			_system.Users.Single().Roles.ShouldBeEmpty();
		}

		[Fact]
		public void Adding_two_permissions_with_the_same_key()
		{
			var perm1 = Permission.Create(CurrentUser, "01", "One", "");
			var perm2 = Permission.Create(CurrentUser, "01", "Two", "");

			_system.AddPermission(perm1);

			Should.Throw<RuleViolationException>(() => _system.AddPermission(perm2));
		}

		[Fact]
		public void Adding_two_roles_with_the_same_key()
		{
			var role1 = Role.Create(CurrentUser, "01", "One", "");
			var role2 = Role.Create(CurrentUser, "01", "Two", "");

			_system.AddRole(role1);

			Should.Throw<RuleViolationException>(() => _system.AddRole(role2));
		}
	}
}
