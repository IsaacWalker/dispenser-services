using Microsoft.Extensions.Diagnostics.HealthChecks;
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


        public PrinterHealthCheck(IPrinterClient printerClient)
        {
            m_printerClient = printerClient;
        }

        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
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

            return new HealthCheckResult(status,"string");
        }
    }
}
