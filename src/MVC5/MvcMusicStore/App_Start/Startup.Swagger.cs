using NSwag.AspNet.Owin;
using Owin;

namespace MvcMusicStore
{
	public partial class Startup
	{
		public void ConfigureSwagger(IAppBuilder app)
		{
			app.UseSwaggerUi(typeof(Startup).Assembly, settings =>
			{
			});
		}
	}
}