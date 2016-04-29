using System.Threading.Tasks;
using Microsoft.Owin;

namespace Magistrate.Api
{
	internal class MagistrateOperatorMiddleware : OwinMiddleware
	{
		private readonly MagistrateConfiguration _config;

		public MagistrateOperatorMiddleware(OwinMiddleware next, MagistrateConfiguration config)
			: base(next)
		{
			_config = config;
		}

		public override async Task Invoke(IOwinContext context)
		{
			context.Set("operator", _config.User());

			await Next.Invoke(context);
		}
	}
}
