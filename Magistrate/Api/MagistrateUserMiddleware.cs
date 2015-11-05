using System.Threading.Tasks;
using Microsoft.Owin;

namespace Magistrate.Api
{
	internal class MagistrateUserMiddleware : OwinMiddleware
	{
		private readonly MagistrateConfiguration _config;

		public MagistrateUserMiddleware(OwinMiddleware next, MagistrateConfiguration config)
			: base(next)
		{
			_config = config;
		}

		public async override Task Invoke(IOwinContext context)
		{
			context.Set("user", _config.User());

			await Next.Invoke(context);
		}
	}
}
