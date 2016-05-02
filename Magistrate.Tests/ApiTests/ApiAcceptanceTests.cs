using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ledger;
using Ledger.Stores;
using Magistrate.Api;
using Magistrate.Domain;
using Magistrate.Domain.Commands;
using Magistrate.Domain.Services;
using MediatR;
using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;
using Owin;
using Shouldly;
using StructureMap;

namespace Magistrate.Tests.ApiTests
{
	public class ApiAcceptanceTests : IDisposable
	{
		private readonly TestServer _server;

		public ApiAcceptanceTests()
		{
			var currentUser = new Operator
			{
				Key = "andy-dote",
				Name = "Andy Dote",
				CanCreatePermissions = true,
				CanCreateRoles = true,
				CanCreateUsers = true
			};

			var config = new MagistrateConfiguration
			{
				EventStore = new InMemoryEventStore(),
				User = () => currentUser
			};

			var container = new Container(new MagistrateRegistry(config.EventStore));

			container.GetInstance<Boot>().Load();

			var ps = container.GetInstance<PermissionService>();
			var rs = container.GetInstance<RoleService>();
			var us = container.GetInstance<UserService>();
			
			var p1 = Permission.Create(ps, currentUser, new PermissionKey("perm-one"), "first", "first permission");
			var p2 = Permission.Create(ps, currentUser, new PermissionKey("perm-two"), "second", "second permission");
			var p3 = Permission.Create(ps, currentUser, new PermissionKey("perm-three"), "third", "third permission");

			var r1 = Role.Create(rs, currentUser, new RoleKey("role-one"), "first", "first role");
			r1.AddPermission(currentUser, p1.ID);

			var r2 = Role.Create(rs, currentUser, new RoleKey("role-two"), "second", "second role");
			r2.AddPermission(currentUser, p2.ID);

			var u1 = User.Create(us, currentUser, new UserKey("user-one"), "first");
			u1.AddRole(currentUser, r1);
			u1.AddInclude(currentUser, p2);
			u1.AddRevoke(currentUser, p3);

			var store = container.GetInstance<AggregateStore<Guid>>();
			
			store.Save(MagistrateSystem.MagistrateStream, p1);
			store.Save(MagistrateSystem.MagistrateStream, p2);
			store.Save(MagistrateSystem.MagistrateStream, p3);
			store.Save(MagistrateSystem.MagistrateStream, r1);
			store.Save(MagistrateSystem.MagistrateStream, r2);
			store.Save(MagistrateSystem.MagistrateStream, u1);

			_server = TestServer.Create(app =>
			{
				app.Use<MagistrateOperatorMiddleware>(config);
				container.GetInstance<PermissionsController>().Configure(app);
				container.GetInstance<RolesController>().Configure(app);
			});
		}

		protected async Task<HttpStatusCode> Get(string url)
		{
			var response = await _server
				.CreateRequest("/api/address")
				.AddHeader("content-type", "application/json")
				.GetAsync();

			var json = await response.Content.ReadAsStringAsync();
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
