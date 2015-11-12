using System;
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

			role.Permissions.ShouldBeEmpty();
		}
	}
}
