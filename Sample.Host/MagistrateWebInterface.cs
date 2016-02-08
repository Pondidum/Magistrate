using System.Reflection;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using React.Owin;

namespace Sample.Host
{
	public class MagistrateWebInterface
	{
		public void Configure(IAppBuilder app)
		{
			//var fs = new PhysicalFileSystem("../../content");
			var fs = new AssemblyResourceFileSystem(Assembly.GetExecutingAssembly(), "Sample.Host.content");

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
