using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Owin;
using webby.Models;

[assembly: OwinStartupAttribute(typeof(webby.Startup))]
namespace webby
{
    public partial class Startup
    {
        

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public IConfiguration Config { get; }
 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<PostContext>(options =>
                options.UseSqlServer(
                    Config.GetConnectionString("DefaultConnection2")));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvcWithDefaultRoute();
            
        }

    }
}
