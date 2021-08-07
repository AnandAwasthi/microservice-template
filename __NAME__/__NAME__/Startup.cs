
using System.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using __NAME__.Modules;
using __NAME__.Shared;


namespace __NAME__
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static BusConfiguration _busConfiguration = (BusConfiguration)ConfigurationManager.GetSection("BusConfiguration");

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutofac();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "__NAME__", Version = "v1" });
            });
            if (_busConfiguration.IsEnabled)
                services.AddHostedService<MassTransitHostedService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<DefaultModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DependencyResolver.Init(app.ApplicationServices);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "__NAME__ v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
