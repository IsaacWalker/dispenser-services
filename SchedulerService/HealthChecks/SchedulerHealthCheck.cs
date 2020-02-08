using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.SchedulerService.Scheduler;

namespace Web.SchedulerService.HealthChecks
{
    public sealed class SchedulerHealthCheck : IHealthCheck
    {
        private readonly SchedulerWorker m_worker;


        private readonly ILogger<SchedulerHealthCheck> m_logger;


        public SchedulerHealthCheck(SchedulerWorker worker, ILogger<SchedulerHealthCheck> logger)
        {
            m_worker = worker;
            m_logger = logger;
        }


        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            m_logger.LogInformation(LogIds.Information.StartSchedulerHealthCheck, "Starting scheduler health check");

            HealthStatus status = m_worker.IsHealthy ? HealthStatus.Healthy : HealthStatus.Unhealthy;
            HealthCheckResult result = new HealthCheckResult(status);

            m_logger.LogInformation(LogIds.Information.EndSchedulerHealthCheck, "Finished scheduler health check: {0}", status);

            return Task.FromResult(result);
        }
    }
}
