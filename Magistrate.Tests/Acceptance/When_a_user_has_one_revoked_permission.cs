using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class When_a_user_has_one_revoked_permission : UserAcceptanceBase
	{
		public When_a_user_has_one_revoked_permission()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);
		}

		[Fact]
		public void AddInclude_a_different_permission_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), SecondPermission);

			User.Includes.ShouldBe(SecondPermissionOnly);
			User.Revokes.ShouldBe(FirstPermissionOnly);
		}

		[Fact]
		public void AddInclude_the_same_permission_removes_from_the_revokes_collection_and_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBe(FirstPermissionOnly);
			User.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_a_different_permission_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), SecondPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBe(FirstPermissionOnly);
		}

		[Fact]
		public void RemoveInclude_the_same_permission_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBe(FirstPermissionOnly);
		}

		[Fact]
		public void AddRevoke_a_different_permission_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), SecondPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBe(BothPermissions);
		}

		[Fact]
		public void AddRevoke_the_same_permission_does_nothing()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBe(FirstPermissionOnly);
		}

		[Fact]
		public void RemoveRevoke_a_different_permission_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), SecondPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBe(FirstPermissionOnly);
		}

		[Fact]
		public void RemoveRevoke_the_same_permission_removes_from_the_revokes_collection()
		{
			User.RemoveRevoke(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBeEmpty();
		}
	}
}
