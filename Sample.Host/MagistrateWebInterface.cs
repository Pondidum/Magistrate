using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Owin;
using React.Owin;

namespace Sample.Host
{
	public class MagistrateWebInterface
	{
		public void Configure(IAppBuilder app)
		{
			var fs = new PhysicalFileSystem("../../content");

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

			//http://stackoverflow.com/a/28457091/1500
			((FileExtensionContentTypeProvider)fileOptions.StaticFileOptions.ContentTypeProvider)
				.Mappings.Add(".woff2", "application/font-woff2");

			app.UseFileServer(fileOptions);
		}
	}
}
