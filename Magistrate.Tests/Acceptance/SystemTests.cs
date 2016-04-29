using System.Net;
using System.Net.Http;
using System.Text;
using Ledger.Stores;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class SystemTests
	{
		[Fact]
		public async void When_creating_a_permission()
		{
			var sys = new MagistrateSystem(new MagistrateConfiguration
			{
				EventStore = new InMemoryEventStore(),
				User = () => new Operator { Key = "user/1", Name = "Andy", CanCreatePermissions = true }
			});

			using (var host = TestServer.Create(sys.Configure))
			{
				var response = await host
					.HttpClient
					.PutAsync("/api/permissions", AsJson(new { Key = "perm-1", Name = "Permission 1", Description = "Permission One" }));

				response.StatusCode.ShouldBe(HttpStatusCode.OK);
			}
		}

		private static StringContent AsJson(object content)
		{
			var json = JsonConvert.SerializeObject(content);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}
	}
}