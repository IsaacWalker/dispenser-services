using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.DispenserClient;
using Web.EntityData;

namespace Web.SchedulerService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ServiceDbContext>(op => op.UseInMemoryDatabase("SchedulerServiceDB"));

            AppContext.SetSwitch(
             "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services.AddGrpcClient<Dispenser.DispenserClient>(o =>
            {
                o.Address = new Uri("http://www.iot3dprint.com:50051");        
            });

            services.AddSingleton<IDispenserClient, RpcDispenserClient>();

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


            app.UseMvc();
        }
    }
}
