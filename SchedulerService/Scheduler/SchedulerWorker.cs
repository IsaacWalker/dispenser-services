using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.EntityData;
using Web.PrinterClient;

namespace Web.SchedulerService.Scheduler
{
    public class SchedulerWorker : BackgroundService
    {

        /// <summary>
        /// Client for communication with the printer
        /// </summary>
        private readonly IPrinterClient m_printerClient;


        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<SchedulerWorker> m_logger;


        /// <summary>
        /// Service provider
        /// </summary>
        private readonly IServiceProvider m_serviceProvider;


        public SchedulerWorker(IServiceProvider serviceProvider, IPrinterClient printerClient, ILogger<SchedulerWorker> logger)
        {
            m_printerClient = printerClient;
            m_logger = logger;
            m_serviceProvider = serviceProvider;
        }


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _isRunning = true;

            while (!stoppingToken.IsCancellationRequested && _isRunning)
            {
                // TODO - Add logic for scheduling the priting...

                using (var scope = m_serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                  //  context.Prescriptions.Where()
                }

                await Task.Delay(1000);
            }

            _isRunning = false;

            return;
        }


        private volatile bool _isRunning;


        public bool IsHealthy => _isRunning;
    }
}
