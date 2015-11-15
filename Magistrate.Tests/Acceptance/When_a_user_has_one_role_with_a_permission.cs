using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class When_a_user_has_one_role_with_a_permission
	{
		[Fact]
		public void AddInclude_a_different_permission_adds_to_the_includes_collection() { }

		[Fact]
		public void AddInclude_the_same_permission_does_nothing() { }

		[Fact]
		public void RemoveInclude_a_different_permission_does_nothing() { }

		[Fact]
		public void RemoveInclude_the_same_permission_does_nothing() { }

		[Fact]
		public void AddRevoke_a_different_permission_adds_to_the_revokes_collection() { }

		[Fact]
		public void AddRevoke_the_same_permission_adds_to_the_revokes_collection() { }

		[Fact]
		public void RemoveRevoke_a_different_permission_does_nothing() { }

		[Fact]
		public void RemoveRevoke_the_same_permission_does_nothing() { }
	}
}