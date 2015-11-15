using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class When_a_user_has_no_permissions_and_no_roles : UserAcceptanceBase
	{
		[Fact]
		public void AddRole_adds_a_role()
		{
			User.AddRole(new MagistrateUser(), TestRole);

			User.Roles.ShouldBe(new[] { TestRole.ID });
		}

		[Fact]
		public void RemoveRole_does_nothing()
		{
			User.RemoveRole(new MagistrateUser(), TestRole);

			User.Roles.ShouldBeEmpty();
		}

		[Fact]
		public void AddInclude_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBe(FirstPermissionOnly);
			User.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void AddRevoke_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);

			User.Revokes.ShouldBe(FirstPermissionOnly);
			User.Includes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveRevoke_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBeEmpty();
		}
	}
}
