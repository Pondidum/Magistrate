using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class UserTests
	{
		private readonly HashSet<Permission> _permissions;
		private Func<Guid, Permission> _getPermission;

		private readonly HashSet<Role> _roles;
		private Func<Guid, Role> _getRole;

		public UserTests()
		{
			_permissions = new HashSet<Permission>();
			_getPermission = id => _permissions.First(p => p.ID == id);

			_roles = new HashSet<Role>();
			_getRole = id => _roles.First(p => p.ID == id);
		}

		private Permission Add(Permission permission)
		{
			_permissions.Add(permission);
			return permission;
		}

		private Role Add(Role role)
		{
			_roles.Add(role);
			return role;
		}


		[Fact]
		public void A_user_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => User.Create(_getPermission, _getRole, "", "No key user")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_user_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => User.Create(_getPermission, _getRole, "some-key", "")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_user_gets_all_properties_assigned()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");

			user.ShouldSatisfyAllConditions(
				() => user.ID.ShouldNotBe(Guid.Empty),
				() => user.Key.ShouldBe("some-key"),
				() => user.Name.ShouldBe("some name")
			);
		}

		[Fact]
		public void A_users_name_cannot_be_removed()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");

			Should.Throw<ArgumentException>(
				() => user.ChangeName("")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_users_name_works()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");

			user.ChangeName("new name");
			user.Name.ShouldBe("new name");
		}

		[Fact]
		public void Adding_a_permission_twice_only_keeps_one()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var permission = Add(Permission.Create("perm-key", "perm_one", "some description"));

			user.AddPermission(permission);
			user.AddPermission(permission);

			user.Includes.ShouldBe(new[] { permission });
			user.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void Removing_a_permission_which_is_included_doesnt_add_a_revoke()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var permission = Add(Permission.Create("perm-key", "perm_one", "some description"));

			user.AddPermission(permission);
			user.Includes.ShouldBe(new[] { permission });
			user.Revokes.ShouldBeEmpty();

			user.RemovePermission(permission);

			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void Removing_a_permission_which_is_not_included_creates_a_revoke()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var permission = Add(Permission.Create("perm-key", "perm_one", "some description"));

			user.RemovePermission(permission);

			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBe(new[] { permission });
		}

		[Fact]
		public void Adding_a_permission_which_is_revoked_removes_the_revoke_and_adds_an_include()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var permission = Add(Permission.Create("perm-key", "perm_one", "some description"));

			user.RemovePermission(permission);
			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBe(new[] { permission });

			user.AddPermission(permission);

			user.Includes.ShouldBe(new[] { permission });
			user.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void A_role_can_be_added_and_removed()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var role = Add(Role.Create(_getPermission, "role-key", "role one", ""));

			user.AddRole(role);
			user.Roles.ShouldBe(new[] { role });

			user.RemoveRole(role);
			user.Roles.ShouldBeEmpty();
		}

		[Fact]
		public void Adding_a_role_twice_only_keeps_one()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var role = Add(Role.Create(_getPermission, "role-key", "role one", ""));

			user.AddRole(role);
			user.AddRole(role);
			user.Roles.ShouldBe(new[] { role });
		}

		[Fact]
		public void When_adding_a_permission_already_included_by_a_role()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var role = Add(Role.Create(_getPermission, "role-key", "role one", ""));
			var permission = Add(Permission.Create("permission-key", "permission one", ""));

			role.AddPermission(permission);
			user.AddRole(role);
			user.AddPermission(permission);

			user.Roles.ShouldBe(new[] { role });
			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void When_revoking_a_permission_included_in_a_role()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var role = Add(Role.Create(_getPermission, "role-key", "role one", ""));
			var permission = Add(Permission.Create("permission-key", "permission one", ""));

			role.AddPermission(permission);
			user.AddRole(role);
			user.RemovePermission(permission);

			user.Roles.ShouldBe(new[] { role });
			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBe(new[] { permission });
		}

		[Fact]
		public void When_revoking_a_permission_included_in_a_role_and_is_included()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var role = Add(Role.Create(_getPermission, "role-key", "role one", ""));
			var permission = Add(Permission.Create("permission-key", "permission one", ""));

			role.AddPermission(permission);

			user.AddPermission(permission);
			user.AddRole(role);
			user.RemovePermission(permission);

			user.Roles.ShouldBe(new[] { role });
			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBe(new[] { permission });
		}

		[Fact]
		public void When_a_user_has_a_revoke_and_gets_a_role_including_the_permission()
		{
			var user = User.Create(_getPermission, _getRole, "some-key", "some name");
			var role = Add(Role.Create(_getPermission, "role-key", "role one", ""));
			var permission = Add(Permission.Create("permission-key", "permission one", ""));

			role.AddPermission(permission);

			user.RemovePermission(permission);
			user.AddRole(role);

			user.Revokes.ShouldBe(new[] { permission });

			user.AddPermission(permission);

			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBeEmpty();
		}
	}
}
