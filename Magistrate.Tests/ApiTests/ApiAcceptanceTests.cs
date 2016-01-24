using System;
using System.Net;
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
using Shouldly;

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
				Name = "Andy Dote",
				CanCreatePermissions = true,
				CanCreateRoles = true,
				CanCreateUsers = true
			};

			var p1 = Permission.Create(currentUser, new PermissionKey("perm-one"), "first", "first permission");
			var p2 = Permission.Create(currentUser, new PermissionKey("perm-two"), "second", "second permission");
			var p3 = Permission.Create(currentUser, new PermissionKey("perm-three"), "third", "third permission");

			var r1 = Role.Create(currentUser, new RoleKey("role-one"), "first", "first role");
			r1.AddPermission(currentUser, p1);

			var r2 = Role.Create(currentUser, new RoleKey("role-two"), "second", "second role");
			r2.AddPermission(currentUser, p2);

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
			store.Save(SystemFacade.MagistrateStream, r2);
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

		protected async Task<HttpStatusCode> Get(string url)
		{
			var response = await _server
				.HttpClient
				.GetAsync(url);

			return response.StatusCode;
		}

		protected async Task<JToken> GetJson(string url)
		{
			return JToken.Parse(await _server.HttpClient.GetStringAsync(url));
		}

		protected async Task<HttpStatusCode> Put(string url, string body)
		{
			var response = await _server
				.HttpClient
				.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

			return response.StatusCode;
		}

		protected async Task<JToken> PutJson(string url, string body)
		{
			var response = await _server
				.HttpClient
				.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

			return JToken.Parse(await response.Content.ReadAsStringAsync());
		}

		protected async Task<HttpStatusCode> Delete(string url, string body)
		{
			var response = await _server
				.CreateRequest(url)
				.And(r => r.Content = new StringContent(body, Encoding.UTF8, "application/json"))
				.SendAsync(HttpMethod.Delete.ToString());

			return response.StatusCode;
		}

		protected void ShouldBeTheSame(JToken actual, JToken expected)
		{
			actual.ToString().ShouldBe(expected.ToString());
		}

		public void Dispose()
		{
			_server.Dispose();
		}
	}
}
