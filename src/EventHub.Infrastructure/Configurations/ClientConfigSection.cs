namespace EventHub.Infrastructure.Configurations
{
    public class ClientConfigSection
    {
        public const string SectionName = "ClientConfig";

        public class AppSettingsSection
        {
            public string Environment { get; set; }

            public string ApiUrl { get; set; }
        }
    }
}
