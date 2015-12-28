using System.Linq;
using Magistrate.Domain;
using Magistrate.Domain.Rules;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain.Rules
{
	public class UniqueKeyRuleTests
	{
		private readonly MagistrateUser _current;

		public UniqueKeyRuleTests()
		{
			_current = new MagistrateUser { Key = "user", Name = "user"};
		}

		[Fact]
		public void When_there_are_no_items()
		{
			var rule = new UniqueKeyRule<Permission, PermissionKey>(Enumerable.Empty<Permission>());
			var add = Permission.Create(_current, new PermissionKey("test"), "test", "");

			rule.IsSatisfiedBy(add).ShouldBe(true);
		}

		[Fact]
		public void When_the_item_clashes_with_another()
		{
			var rule = new UniqueKeyRule<Permission, PermissionKey>(new[]
			{
				Permission.Create(_current, new PermissionKey("test"), "Already In", "")
			});

			var add = Permission.Create(_current, new PermissionKey("test"), "New", "");

			rule.IsSatisfiedBy(add).ShouldBe(false);
		}

		[Fact]
		public void When_the_item_is_already_in_the_collection()
		{
			var perm = Permission.Create(_current, new PermissionKey("test"), "Already In", "");
            var rule = new UniqueKeyRule<Permission, PermissionKey>(new[] { perm });

			rule.IsSatisfiedBy(perm).ShouldBe(true);
		}
	}
}
