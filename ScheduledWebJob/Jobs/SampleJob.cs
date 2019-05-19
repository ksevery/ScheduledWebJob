using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduledWebJob.Jobs
{
    public class SampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            // Implementation of job goes here, supports DI and can do basically anything, will be executed based on trigger definition
            return Task.FromResult(5);
        }
    }
}
