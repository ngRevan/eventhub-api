namespace EventHub.Infrastructure.Configurations
{
    public class DataProtectionSection
    {
        public const string SectionName = "DataProtection";

        public CertificateSection Certificate { get; set; }

        public class CertificateSection
        {
            public string FilePath { get; set; }

            public string Password { get; set; }
        }
    }
}
