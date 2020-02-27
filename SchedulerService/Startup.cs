using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client.Web;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.EntityData;
using Web.PrinterClient;
using Web.SchedulerService.HealthChecks;
using Web.SchedulerService.Medication;
using Web.SchedulerService.Scheduler;

namespace Web.SchedulerService
{
    public class Startup
    {
        private readonly IConfiguration m_configuration;


        public Startup(IConfiguration configuration)
        {
            m_configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
              services.AddDbContext<ServiceDbContext>(op => op.UseInMemoryDatabase(m_configuration["SchedulerService:DatabaseName"]));
               services.AddLogging();

                AppContext.SetSwitch(
                 "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                services.AddGrpcClient<Printer.PrinterClient>(o =>
                {
                    o.Address = new Uri(m_configuration["SchedulerService:PrinterUrl"]);        
                });

                services.AddSingleton<IODFGenerator, ODFGenerator>();
                services.AddSingleton<IPrintingContext, PrintingContext>();

                services.AddSingleton<SchedulerWorker>();
                services.AddHostedService((sp) => sp.GetService<SchedulerWorker>());

            //adding health check services to container
            services
                .AddHealthChecksUI()
                .AddHealthChecks()
                .AddCheck<PrinterHealthCheck>(nameof(PrinterHealthCheck))
                .AddCheck<SchedulerHealthCheck>(nameof(SchedulerHealthCheck))
                .Services
                .AddControllers();


            services.AddMvc().AddMvcOptions(options => options.EnableEndpointRouting = false);

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          
            app
            .UseRouting()
            .UseEndpoints(config =>
                {
            config.MapHealthChecks("healthz", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
                config.MapHealthChecksUI();

                 config.MapDefaultControllerRoute();
            });
        }
    }
}
