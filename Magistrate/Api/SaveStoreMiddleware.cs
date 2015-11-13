using System.Threading.Tasks;
using Magistrate.Domain;
using Microsoft.Owin;

namespace Magistrate.Api
{
	public class SaveStoreMiddleware : OwinMiddleware
	{
		private readonly MagistrateSystem _system;

		public SaveStoreMiddleware(OwinMiddleware next, MagistrateSystem system) 
			: base(next)
		{
			_system = system;
		}

		public override async Task Invoke(IOwinContext context)
		{
			await Next.Invoke(context);
			_system.Save();
		}
	}
}