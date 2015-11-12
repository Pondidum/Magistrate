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
			var rule = new UniqueKeyRule<Permission>(Enumerable.Empty<Permission>());
			var add = Permission.Create(_current, "test", "test", "");

			rule.IsSatisfiedBy(add).ShouldBe(true);
		}

		[Fact]
		public void When_the_item_clashes_with_another()
		{
			var rule = new UniqueKeyRule<Permission>(new[]
			{
				Permission.Create(_current, "test", "Already In", "")
			});

			var add = Permission.Create(_current, "test", "New", "");

			rule.IsSatisfiedBy(add).ShouldBe(false);
		}

		[Fact]
		public void When_the_item_is_already_in_the_collection()
		{
			var perm = Permission.Create(_current, "test", "Already In", "");
            var rule = new UniqueKeyRule<Permission>(new[] { perm });

			rule.IsSatisfiedBy(perm).ShouldBe(true);
		}
	}
}
