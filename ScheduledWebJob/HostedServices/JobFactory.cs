using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduledWebJob.HostedServices
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private IServiceScope _scope;
        public JobFactory(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            _scope = _scopeFactory.CreateScope();

            var job = _scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            return job;
        }

        public void ReturnJob(IJob job)
        {
            _scope.Dispose();
            GC.Collect();
        }
    }
}
