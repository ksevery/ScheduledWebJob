using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduledWebJob.QuartzConfiguration
{
    public class QuartzOptions
    {
        public QuartzOptions(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var section = config.GetSection("quartz");
            section.Bind(this);
        }

        public Scheduler Scheduler { get; set; }

        public ThreadPool ThreadPool { get; set; }

        public Plugin Plugin { get; set; }

        public NameValueCollection ToProperties()
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = Scheduler?.InstanceName,
                ["quartz.threadPool.type"] = ThreadPool?.Type,
                ["quartz.threadPool.threadPriority"] = ThreadPool?.ThreadPriority,
                ["quartz.threadPool.threadCount"] = ThreadPool?.ThreadCount.ToString(),
                ["quartz.plugin.jobInitializer.type"] = Plugin?.JobInitializer?.Type,
                ["quartz.plugin.jobInitializer.fileNames"] = Plugin?.JobInitializer?.FileNames
            };

            return properties;
        }
    }
}
