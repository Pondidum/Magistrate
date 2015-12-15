using System.Linq;
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

			Project(User);

			ReadModel.Users.Single().Includes.Single().ID.ShouldBe(SecondPermissionOnly.Single());
			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
		}

		[Fact]
		public void AddInclude_the_same_permission_removes_from_the_revokes_collection_and_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_a_different_permission_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), SecondPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
		}

		[Fact]
		public void RemoveInclude_the_same_permission_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
		}

		[Fact]
		public void AddRevoke_a_different_permission_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), SecondPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.Select(p => p.ID).ShouldBe(BothPermissions);
		}

		[Fact]
		public void AddRevoke_the_same_permission_does_nothing()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
		}

		[Fact]
		public void RemoveRevoke_a_different_permission_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), SecondPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
		}

		[Fact]
		public void RemoveRevoke_the_same_permission_removes_from_the_revokes_collection()
		{
			User.RemoveRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}
	}
}
