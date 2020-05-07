namespace EventHub.Infrastructure.Configurations
{
    public class DistributedCacheSection
    {
        public const string SectionName = "DistributedCache";

        public string Configuration { get; set; }

        public string InstanceName { get; set; }
    }
}
