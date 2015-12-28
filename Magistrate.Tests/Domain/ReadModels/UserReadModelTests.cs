using Magistrate.Domain;
using Magistrate.Domain.ReadModels;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain.ReadModels
{
	public class UserReadModelTests
	{
		private readonly PermissionReadModel _permissionOne;
		private readonly PermissionReadModel _permissionTwo;

		public UserReadModelTests()
		{
			_permissionOne = new PermissionReadModel { Name = "One", Key = new PermissionKey("perm-one") };
			_permissionTwo = new PermissionReadModel { Name = "Two", Key = new PermissionKey("perm-two") };
		}

		[Fact]
		public void When_the_user_has_no_permissions()
		{
			var user = new UserReadModel();

			user.Can(_permissionOne.Key).ShouldBe(false);
		}

		[Fact]
		public void When_the_user_has_an_include()
		{
			var user = new UserReadModel();
			user.Includes.Add(_permissionOne);

			user.Can(_permissionOne.Key).ShouldBe(true);
			user.Can(_permissionTwo.Key).ShouldBe(false);
		}

		[Fact]
		public void When_the_user_has_a_revoke()
		{
			var user = new UserReadModel();
			user.Revokes.Add(_permissionOne);

			user.Can(_permissionOne.Key).ShouldBe(false);
		}

		[Fact]
		public void When_the_user_has_a_role()
		{
			var role = new RoleReadModel();
			role.Permissions.Add(_permissionOne);

			var user = new UserReadModel();
			user.Roles.Add(role);

			user.Can(_permissionOne.Key).ShouldBe(true);
			user.Can(_permissionTwo.Key).ShouldBe(false);
		}

		[Fact]
		public void When_the_user_has_a_revoked_permission_included_by_a_role()
		{
			var role = new RoleReadModel();
			role.Permissions.Add(_permissionOne);

			var user = new UserReadModel();
			user.Roles.Add(role);
			user.Revokes.Add(_permissionOne);

			user.Can(_permissionOne.Key).ShouldBe(false);
			user.Can(_permissionTwo.Key).ShouldBe(false);
		}
	}
}
