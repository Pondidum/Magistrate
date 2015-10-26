using System.Diagnostics;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using React.Owin;

namespace Sample.Host
{
	public class MagistrateWebInterface
	{
		public MagistrateWebInterface()
		{

		}

		public void Configure(IAppBuilder app)
		{

			var root = Debugger.IsAttached
				? "../../content"
				: "content";

			var fs = new PhysicalFileSystem(root);

			app.UseBabel(new BabelFileOptions
			{
				StaticFileOptions = new StaticFileOptions
				{
					FileSystem = fs
				}
			});

			app.UseFileServer(new FileServerOptions
			{
				FileSystem = fs,
				EnableDefaultFiles = true,
				DefaultFilesOptions = { DefaultFileNames = { "app.htm" } }
			});
		}
	}
}
