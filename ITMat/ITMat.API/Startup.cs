using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ITMat.Core.Integrations;
using NLog.Web;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.Http;

namespace ITMat.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
            services.AddLogging(logger => logger.AddNLog("nlog.config"));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.UseCamelCasing(true)
            );

            services.AddITMatCore();

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            //Needed for aspnet-request-posted-body to work for NLog
            //https://github.com/NLog/NLog.Web/issues/556#issuecomment-628262170
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );
        }
    }
}