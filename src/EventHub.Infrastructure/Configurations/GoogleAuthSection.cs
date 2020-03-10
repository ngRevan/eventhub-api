namespace EventHub.Infrastructure.Configurations
{
    public class GoogleAuthSection
    {
        public const string SectionName = "Authentication:Google";

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
