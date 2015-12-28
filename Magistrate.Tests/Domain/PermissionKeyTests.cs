using Magistrate.Domain;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class PermissionKeyTests
	{
		[Fact]
		public void Two_keys_with_same_value()
		{
			var one = new PermissionKey("one");
			var two = new PermissionKey("one");

			one.ShouldBe(two);
		}

		[Fact]
		public void Two_keys_with_different_values_by_case()
		{
			var one = new PermissionKey("one");
			var two = new PermissionKey("ONE");

			one.ShouldBe(two);
		}

		[Fact]
		public void Two_different_keys()
		{
			var one = new PermissionKey("one");
			var two = new PermissionKey("two");

			one.ShouldNotBe(two);
		}

		[Fact]
		public void When_serializing()
		{
			var input = new PermissionKey("one");
			var json = JsonConvert.SerializeObject(input);
			var output = JsonConvert.DeserializeObject<PermissionKey>(json);

			output.ShouldBe(input);
		}

		public class RoleKeyTests
		{
			[Fact]
			public void Two_keys_with_same_value()
			{
				var one = new RoleKey("one");
				var two = new RoleKey("one");

				one.ShouldBe(two);
			}

			[Fact]
			public void Two_keys_with_different_values_by_case()
			{
				var one = new RoleKey("one");
				var two = new RoleKey("ONE");

				one.ShouldBe(two);
			}

			[Fact]
			public void Two_different_keys()
			{
				var one = new RoleKey("one");
				var two = new RoleKey("two");

				one.ShouldNotBe(two);
			}

			[Fact]
			public void When_serializing()
			{
				var input = new RoleKey("one");
				var json = JsonConvert.SerializeObject(input);
				var output = JsonConvert.DeserializeObject<RoleKey>(json);

				output.ShouldBe(input);
			}
		}
	}
}