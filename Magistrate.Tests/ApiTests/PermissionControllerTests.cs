﻿using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.ApiTests
{
	public class PermissionControllerTests : ApiAcceptanceTests
	{
		[Fact]
		public async void When_listing_all_permissions()
		{
			var response = await  Get("/api/permissions/all");

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
		public async void When_listing_all_roles()
		{
			var response = await Get("/api/roles/all");

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

			JToken.DeepEquals(response, expected).ShouldBe(true, response.ToString);
		}

		[Fact]
		public async void When_listing_all_users()
		{
			var response = await Get("/api/users/all");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""user-one"",
    ""name"": ""first"",
    ""isActive"": false,
    ""includes"": [
      {
        ""key"": ""perm-two"",
        ""name"": ""second"",
        ""description"": ""second permission""
      }
    ],
    ""revokes"": [
      {
        ""key"": ""perm-three"",
        ""name"": ""third"",
        ""description"": ""third permission""
      }
    ],
    ""roles"": [
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
  }
]
");

			JToken.DeepEquals(response, expected).ShouldBe(true, response.ToString);
		}
	}
}
