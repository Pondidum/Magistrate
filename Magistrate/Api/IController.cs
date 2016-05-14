using Owin;

namespace Magistrate.Api
{
	public interface IController
	{
		void Configure(IAppBuilder app);
	}
}
