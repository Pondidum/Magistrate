using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.ApiTests
{
	public class PermissionControllerTests : ApiAcceptanceTests
	{
		[Fact]
		public async void When_listing_all_permissions()
		{
			var response = await Get("/api/permissions/all");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""perm-one"",
    ""name"": ""first"",
    ""description"": ""first permission""
  },
  {
    ""key"": ""perm-two"",
    ""name"": ""second"",
    ""description"": ""second permission""
  },
  {
    ""key"": ""perm-three"",
    ""name"": ""third"",
    ""description"": ""third permission""
  }
]
");

			JToken.DeepEquals(response, expected).ShouldBe(true, response.ToString);
		}

		[Fact]
		public async void When_creating_a_permission()
		{
			var response = await PutJson("/api/permissions", @"
{
  ""key"":""perm-new"",
  ""name"":""New Permission"",
  ""description"":""A new permission"",
}
");
			var expected = JToken.Parse(@"
  {
    ""key"": ""perm-new"",
    ""name"": ""New Permission"",
    ""description"": ""A new permission""
  }
");

			JToken.DeepEquals(response, expected).ShouldBe(true, response.ToString);
		}
	}
}
