using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.PrinterClient;

namespace Web.SchedulerService.HealthChecks
{
    public sealed class PrinterHealthCheck : IHealthCheck
    {
        private readonly IPrinterClient m_printerClient;


        private readonly ILogger<PrinterHealthCheck> m_logger;


        public PrinterHealthCheck(IPrinterClient printerClient, ILogger<PrinterHealthCheck> logger)
        {
            m_printerClient = printerClient;
            m_logger = logger;
        }

        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            m_logger.LogInformation(LogIds.Information.StartPrinterHealthCheck, "Starting printer health check");


            var response = await m_printerClient.CheckPrinterHealthAsync(new CheckPrinterHealthRequest());

            HealthStatus status;

            if(response.Status == CheckPrinterHealthResponse.Types.HealthCheckStatus.Healthy)
            {
                status = HealthStatus.Healthy;
            }
            else
            {
                status = HealthStatus.Unhealthy;
            }

            m_logger.LogInformation(LogIds.Information.EndtPrinterHealthCheck, "Finished printer health check: {0}", status);

            return new HealthCheckResult(status, string.Format("Printer: {0} ", response.Status));
        }
    }
}
