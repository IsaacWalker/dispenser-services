using Microsoft.Extensions.Diagnostics.HealthChecks;
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


        public SchedulerHealthCheck(SchedulerWorker worker)
        {
            m_worker = worker;
        }


        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult
                (
                new HealthCheckResult((m_worker.IsHealthy ? HealthStatus.Healthy : HealthStatus.Unhealthy)
                ));
        }
    }
}
