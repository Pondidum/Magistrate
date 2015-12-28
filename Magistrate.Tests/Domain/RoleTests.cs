using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class RolesTests
	{
		private readonly MagistrateUser _currentUser;

		public RolesTests()
		{
			_currentUser = new MagistrateUser { Name = "Current User", Key = "cu" };
		}

		[Fact]
		public void A_role_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => Role.Create(_currentUser, new RoleKey(""), "No key role", "doesnt have a key")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_role_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => Role.Create(_currentUser, new RoleKey("some-key"), "", "doesnt have a name")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_role_doesnt_need_a_description()
		{
			var role = Role.Create(_currentUser, new RoleKey("some-key"), "some-name", "");

			role.Description.ShouldBeEmpty();
		}

		[Fact]
		public void A_role_gets_all_properties_assigned()
		{
			var role = Role.Create(_currentUser, new RoleKey("some-key"), "some name", "some description");

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
			var role = Role.Create(_currentUser, new RoleKey("some-key"), "some name", "some description");

			Should.Throw<ArgumentException>(
				() => role.ChangeName(_currentUser, "")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_roles_name_works()
		{
			var role = Role.Create(_currentUser, new RoleKey("some-key"), "some name", "some description");

			role.ChangeName(_currentUser, "new name");
			role.Name.ShouldBe("new name");
		}

		[Fact]
		public void Changing_a_roles_description_works()
		{
			var role = Role.Create(_currentUser, new RoleKey("some-key"), "some name", "some description");

			role.ChangeDescription(_currentUser, "new description");
			role.Description.ShouldBe("new description");
		}

		[Fact]
		public void A_permission_can_be_added_and_removed()
		{
			var role = Role.Create(_currentUser, new RoleKey("some-key"), "some name", "some description");
			var permission = Permission.Create(_currentUser, new PermissionKey("key"), "permission_one", "");

			role.AddPermission(_currentUser, permission);
			role.Permissions.ShouldBe(new[] { permission.ID });

			role.RemovePermission(_currentUser, permission);
			role.Permissions.ShouldBeEmpty();
		}
	}
}
