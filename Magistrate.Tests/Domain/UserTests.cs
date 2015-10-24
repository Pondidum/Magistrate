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

		public UserTests()
		{
			_permissions = new HashSet<Permission>();
			_getPermission = id => _permissions.First(p => p.ID == id);
		}

		private Permission Add(Permission permission)
		{
			_permissions.Add(permission);
			return permission;
		}


		[Fact]
		public void A_user_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => User.Create(_getPermission, "", "No key user")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_user_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => User.Create(_getPermission, "some-key", "")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_user_gets_all_properties_assigned()
		{
			var user = User.Create(_getPermission, "some-key", "some name");

			user.ShouldSatisfyAllConditions(
				() => user.ID.ShouldNotBe(Guid.Empty),
				() => user.Key.ShouldBe("some-key"),
				() => user.Name.ShouldBe("some name")
			);
		}

		[Fact]
		public void A_users_name_cannot_be_removed()
		{
			var user = User.Create(_getPermission, "some-key", "some name");

			Should.Throw<ArgumentException>(
				() => user.ChangeName("")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_users_name_works()
		{
			var user = User.Create(_getPermission, "some-key", "some name");

			user.ChangeName("new name");
			user.Name.ShouldBe("new name");
		}

		[Fact]
		public void Adding_a_permission_twice_only_keeps_one()
		{
			var user = User.Create(_getPermission, "some-key", "some name");
			var permission = Add(Permission.Create("perm-key", "perm_one", "some description"));

			user.AddPermission(permission);
			user.AddPermission(permission);

			user.Includes.ShouldBe(new[] { permission });
			user.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void Removing_a_permission_which_is_included_doesnt_add_a_revoke()
		{
			var user = User.Create(_getPermission, "some-key", "some name");
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
			var user = User.Create(_getPermission, "some-key", "some name");
			var permission = Add(Permission.Create("perm-key", "perm_one", "some description"));

			user.RemovePermission(permission);

			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBe(new[] { permission });
		}

		[Fact]
		public void Adding_a_permission_which_is_revoked_removes_the_revoke_and_adds_an_include()
		{
			var user = User.Create(_getPermission, "some-key", "some name");
			var permission = Add(Permission.Create("perm-key", "perm_one", "some description"));

			user.RemovePermission(permission);
			user.Includes.ShouldBeEmpty();
			user.Revokes.ShouldBe(new[] { permission });

			user.AddPermission(permission);

			user.Includes.ShouldBe(new[] { permission });
			user.Revokes.ShouldBeEmpty();
		}
	}
}
