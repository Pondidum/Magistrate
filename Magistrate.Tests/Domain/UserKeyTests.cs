using Magistrate.Domain;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class UserKeyTests
	{
		[Fact]
		public void Two_keys_with_same_value()
		{
			var one = new UserKey("one");
			var two = new UserKey("one");

			one.ShouldBe(two);
		}

		[Fact]
		public void Two_keys_with_different_values_by_case()
		{
			var one = new UserKey("one");
			var two = new UserKey("ONE");

			one.ShouldBe(two);
		}

		[Fact]
		public void Two_different_keys()
		{
			var one = new UserKey("one");
			var two = new UserKey("two");

			one.ShouldNotBe(two);
		}

		[Fact]
		public void When_serializing()
		{
			var input = new UserKey("one");
			var json = JsonConvert.SerializeObject(input);
			var output = JsonConvert.DeserializeObject<UserKey>(json);

			output.ShouldBe(input);
		}
	}
}
