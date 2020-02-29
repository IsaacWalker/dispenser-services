using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.DispenserClient;

namespace Web.SchedulerService.HealthChecks
{
    public sealed class PrinterHealthCheck : IHealthCheck
    {
        private readonly IDispenserClient m_dispenserClient;


        private readonly ILogger<PrinterHealthCheck> m_logger;


        public PrinterHealthCheck(IDispenserClient dispenserClient, ILogger<PrinterHealthCheck> logger)
        {
            m_dispenserClient = dispenserClient;
            m_logger = logger;
        }

        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            m_logger.LogInformation(LogIds.Information.StartPrinterHealthCheck, "Starting printer health check");


            var response = await m_dispenserClient.GetPrinterStatusAsync();

            HealthStatus status;

            if(response.Status != PrinterStatus.NO_CONNECTION)
            {
                status = HealthStatus.Healthy;
            }
            else
            {
                status = HealthStatus.Unhealthy;
            }

            m_logger.LogInformation(LogIds.Information.EndtPrinterHealthCheck, "Finished printer health check: {0}", status);

            return new HealthCheckResult(status, string.Format("Printer: {0} ", response));
        }
    }
}
