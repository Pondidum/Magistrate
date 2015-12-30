using Newtonsoft.Json.Linq;
using Xunit;

namespace Magistrate.Tests.ApiTests
{
	public class RoleControllerTests : ApiAcceptanceTests
	{
		[Fact]
		public async void When_listing_all_roles()
		{
			var response = await GetJson("/api/roles/all");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""role-one"",
    ""name"": ""first"",
    ""description"": ""first role"",
    ""permissions"": [
      {
        ""key"": ""perm-one"",
        ""name"": ""first"",
        ""description"": ""first permission""
      }
    ]
  }
]
");

			ShouldBeTheSame(response, expected);
		}
	}
}
