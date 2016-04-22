using Magistrate.Domain;
using Magistrate.Domain.ReadModels;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class UserTests
	{
		private readonly User _user;
		private MagistrateUser _cu;
		private Permission _permissionOne;
		private Permission _permissionTwo;

		public UserTests()
		{
			_cu = new MagistrateUser
			{
				CanCreatePermissions = true,
				CanCreateRoles = true,
				CanCreateUsers = true
			};

			_user = User.Blank();

			_permissionOne = Permission.Create(_cu, new PermissionKey("perm-one"), "One", "");
			_permissionTwo = Permission.Create(_cu, new PermissionKey("perm-two"), "Two", "");
		}

		[Fact]
		public void When_the_user_has_no_permissions()
		{
			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBeEmpty(),
				() => _user.Roles.ShouldBeEmpty()
			);
		}

		[Fact]
		public void When_the_user_has_an_include()
		{
			_user.AddInclude(_cu, _permissionOne);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBe(new[] { _permissionOne.ID }),
				() => _user.Revokes.ShouldBeEmpty(),
				() => _user.Roles.ShouldBeEmpty()
			);
		}

		[Fact]
		public void When_the_user_has_a_revoke()
		{
			_user.AddRevoke(_cu, _permissionOne);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBe(new[] { _permissionOne.ID }),
				() => _user.Roles.ShouldBeEmpty()
			);
		}

		[Fact]
		public void When_the_user_has_a_role()
		{
			var role = Role.Create(_cu, new RoleKey("role-one"), "role one", "");
			role.AddPermission(_cu, _permissionOne);

			_user.AddRole(_cu, role);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBeEmpty(),
				() => _user.Roles.ShouldBe(new [] { role.ID })
			);
		}

		[Fact]
		public void When_the_user_has_a_revoked_permission_included_by_a_role()
		{
			var role = Role.Create(_cu, new RoleKey("role-one"), "role one", "");
			role.AddPermission(_cu, _permissionOne);

			_user.AddRole(_cu, role);
			_user.AddRevoke(_cu, _permissionOne);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBe(new[] { _permissionOne.ID }),
				() => _user.Roles.ShouldBe(new[] { role.ID })
			);
		}
	}
}
