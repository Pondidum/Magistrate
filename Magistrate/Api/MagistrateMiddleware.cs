using System.Threading.Tasks;
using Magistrate.Domain;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class MagistrateTopware
	{
		private readonly Store _store;

		public MagistrateTopware(Store store)
		{
			_store = store;
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/permissions/all").Get(async context =>
			{
				await context.WriteJson(_store.Permissions.AllPermissions);
			});
		}
	}
}
