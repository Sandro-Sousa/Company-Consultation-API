
namespace Entities
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new();
        public static EmailConfiguration Email { get; set; } = new();
        public static SendGridConfiguration SendGrid { get; set; } = new();
        public static DatabaseConfiguration Database { get; set; } = new();
        public static SecretsKeysIntegrationPayment KeyPayment { get; set; } = new();

        public class DatabaseConfiguration
        {
            public string ConnectionString { get; set; } = string.Empty;
        }

        public class EmailConfiguration
        {
            public string DefaultFromEmail { get; set; } = "sandrodev30@gmail.com";
            public string DefaultFromName { get; set; } = "sandro";
            public string DefaultFromPassword { get; set; } = "vfbh rlss vahn tsks";
        }

        public class SendGridConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
        }

        public class SecretsConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
            public string JwtPrivateKey { get; set; } = string.Empty;
            public string PasswordSaltKey { get; set; } = string.Empty;
        }

        public class SecretsKeysIntegrationPayment
        {
            public string PublishableKey { get; set; } = string.Empty;
            public string SecretKey { get; set; } = string.Empty;
        }
    }
}
