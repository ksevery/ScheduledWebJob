namespace ScheduledWebJob.QuartzConfiguration
{
    public class ThreadPool
    {
        public string Type { get; set; }

        public string ThreadPriority { get; set; }

        public int ThreadCount { get; set; }
    }
}