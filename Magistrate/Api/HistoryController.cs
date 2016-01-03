using System.Threading.Tasks;
using Magistrate.Domain.Services;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class HistoryController : Controller
	{
		public HistoryController(SystemFacade system)
			: base(system)
		{
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/history/all").Get(GetAllHistory);
		}

		private async Task GetAllHistory(IOwinContext context)
		{
			await context.JsonResponse(System.History);
		}
	}
}
