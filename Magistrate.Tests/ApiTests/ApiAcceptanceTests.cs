using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ledger;
using Ledger.Stores;
using Magistrate.Api;
using Magistrate.Domain;
using Magistrate.Domain.Services;
using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;

namespace Magistrate.Tests.ApiTests
{
	public class ApiAcceptanceTests : IDisposable
	{
		private readonly TestServer _server;

		public ApiAcceptanceTests()
		{
			var currentUser = new MagistrateUser
			{
				Key = "andy-dote",
				Name = "Andy Dote"
			};

			var p1 = Permission.Create(currentUser, new PermissionKey("perm-one"), "first", "first permission");
			var p2 = Permission.Create(currentUser, new PermissionKey("perm-two"), "second", "second permission");
			var p3 = Permission.Create(currentUser, new PermissionKey("perm-three"), "third", "third permission");

			var r1 = Role.Create(currentUser, new RoleKey("role-one"), "first", "first role");
			r1.AddPermission(currentUser, p1);

			var u1 = User.Create(currentUser, new UserKey("user-one"), "first");
			u1.AddRole(currentUser, r1);
			u1.AddInclude(currentUser, p2);
			u1.AddRevoke(currentUser, p3);

			var events = new InMemoryEventStore();
			var store = new AggregateStore<Guid>(events);
			store.Save(SystemFacade.MagistrateStream, p1);
			store.Save(SystemFacade.MagistrateStream, p2);
			store.Save(SystemFacade.MagistrateStream, p3);
			store.Save(SystemFacade.MagistrateStream, r1);
			store.Save(SystemFacade.MagistrateStream, u1);

			var config = new MagistrateConfiguration
			{
				EventStore = events,
				User = () => currentUser
			};

			var api = new MagistrateApi(config);
			_server = TestServer.Create(app =>
			{
				api.Configure(app);
			});
		}

		protected async Task<JToken> Get(string url)
		{
			return JToken.Parse(await _server.HttpClient.GetStringAsync(url));
		}

		protected async Task<JToken> PutJson(string url, string body)
		{
			var response = await _server
				.HttpClient
				.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

			return JToken.Parse(await response.Content.ReadAsStringAsync());
		}

		public void Dispose()
		{
			_server.Dispose();
		}
	}
}
