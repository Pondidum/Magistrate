using System;
using System.Linq;
using Ledger.Infrastructure;
using Magistrate.Domain;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Services;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class RolesTests
	{
		private readonly Operator _currentUser;
		private readonly RoleService _roleService;
		private readonly PermissionService _permissionService;

		public RolesTests()
		{
			_roleService = new RoleService();
			_permissionService = new PermissionService();

			_currentUser = new Operator
			{
				Name = "Current User",
				Key = "cu",
				CanCreatePermissions = true,
				CanCreateRoles = true
			};
		}

		[Fact]
		public void A_role_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => Role.Create(_roleService, _currentUser, new RoleKey(""), "No key role", "doesnt have a key")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_role_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => Role.Create(_roleService, _currentUser, new RoleKey("some-key"), "", "doesnt have a name")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_role_doesnt_need_a_description()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("some-key"), "some-name", "");

			role.Description.ShouldBeEmpty();
		}

		[Fact]
		public void A_role_gets_all_properties_assigned()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("some-key"), "some name", "some description");

			role.ShouldSatisfyAllConditions(
				() => role.ID.ShouldNotBe(Guid.Empty),
				() => role.Key.ShouldBe(new RoleKey("some-key")),
				() => role.Name.ShouldBe("some name"),
				() => role.Description.ShouldBe("some description")
			);
		}

		[Fact]
		public void A_roles_name_cannot_be_removed()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("some-key"), "some name", "some description");

			Should.Throw<ArgumentException>(
				() => role.ChangeName(_currentUser, "")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_roles_name_works()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("some-key"), "some name", "some description");

			role.ChangeName(_currentUser, "new name");
			role.Name.ShouldBe("new name");
		}

		[Fact]
		public void Changing_a_roles_description_works()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("some-key"), "some name", "some description");

			role.ChangeDescription(_currentUser, "new description");
			role.Description.ShouldBe("new description");
		}

		[Fact]
		public void A_permission_can_be_added_and_removed()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("some-key"), "some name", "some description");
			var permission = Permission.Create(_permissionService, _currentUser, new PermissionKey("key"), "permission_one", "");

			role.AddPermission(_currentUser, permission.ID);
			role.Permissions.ShouldBe(new[] { permission.ID });

			role.RemovePermission(_currentUser, permission.ID);
			role.Permissions.ShouldBeEmpty();
		}

		[Fact]
		public void Multiple_roles_with_the_same_key_cannot_be_created()
		{
			var service = new RoleService();

			var r1 = Role.Create(service, _currentUser, new RoleKey("1"), "name", "desc");

			//simulate saving to the eventstore
			r1.GetUncommittedEvents().ForEach(service.Project);

			Should.Throw<ArgumentException>(() => Role.Create(service, _currentUser, new RoleKey("1"), "new", "newer"));
		}

		[Fact]
		public void Adding_the_same_permission_multiple_times_to_a_role_does_nothing()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("role"), "the role", "");
			var permission = Permission.Create(_permissionService, _currentUser, new PermissionKey("permission"), "the permission", "");

			role.AddPermission(_currentUser, permission.ID);
			role.AddPermission(_currentUser, permission.ID);

			role.GetUncommittedEvents().Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(RoleCreatedEvent),
				typeof(PermissionAddedToRoleEvent)
			});
		}

		[Fact]
		public void Removing_the_same_permission_multiple_times_from_a_role_does_nothing()
		{
			var role = Role.Create(_roleService, _currentUser, new RoleKey("role"), "the role", "");
			var permission = Permission.Create(_permissionService, _currentUser, new PermissionKey("permission"), "the permission", "");

			role.AddPermission(_currentUser, permission.ID);
			role.RemovePermission(_currentUser, permission.ID);
			role.RemovePermission(_currentUser, permission.ID);

			role.GetUncommittedEvents().Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(RoleCreatedEvent),
				typeof(PermissionAddedToRoleEvent),
				typeof(PermissionRemovedFromRoleEvent)
			});
		}
	}
}
