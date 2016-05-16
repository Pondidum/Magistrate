using System.Diagnostics;
using System.Reflection;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace Magistrate.WebInterface
{
	public class MagistrateWebInterface
	{
		public void Configure(IAppBuilder app)
		{
			IFileSystem fs;

			if (Debugger.IsAttached)
				fs = new PhysicalFileSystem("../../../Magistrate/client");
			else
				fs = new AssemblyResourceFileSystem(Assembly.GetExecutingAssembly(), "Magistrate.client");

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
