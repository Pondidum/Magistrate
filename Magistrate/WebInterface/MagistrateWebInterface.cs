using System.Reflection;
using Microsoft.Owin.StaticFiles;
using Owin;
using React.Owin;

namespace Magistrate.WebInterface
{
	public class MagistrateWebInterface
	{
		public void Configure(IAppBuilder app)
		{
			//var fs = new PhysicalFileSystem("../../content");
			var fs = new AssemblyResourceFileSystem(Assembly.GetExecutingAssembly(), "Magistrate.content");

			app.UseBabel(new BabelFileOptions
			{
				StaticFileOptions = new StaticFileOptions
				{
					FileSystem = fs
				},
				Extensions = new[] { ".jsx" }
			});

			var fileOptions = new FileServerOptions
			{
				FileSystem = fs,
				EnableDefaultFiles = true,
				DefaultFilesOptions = { DefaultFileNames = { "app.htm" } }
			};

			app.UseFileServer(fileOptions);
		}
	}
}
