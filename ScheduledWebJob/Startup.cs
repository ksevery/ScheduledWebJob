using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz.Impl;
using Quartz.Spi;
using ScheduledWebJob.HostedServices;
using ScheduledWebJob.Jobs;
using ScheduledWebJob.QuartzConfiguration;

namespace ScheduledWebJob
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            // IMPORTANT: Do not forget to register the job itself so it can be later resolved by the JobFactory
            services.AddTransient<SampleJob>();
            services.AddTransient<IJobFactory, JobFactory>();
            services.AddSingleton(provider =>
            {
                var option = new QuartzOptions(Configuration);
                var sf = new StdSchedulerFactory(option.ToProperties());
                var scheduler = sf.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                return scheduler;
            });

            services.AddHostedService<SchedulingHostedService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
