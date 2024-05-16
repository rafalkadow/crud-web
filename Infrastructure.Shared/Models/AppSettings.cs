namespace Infrastructure.Shared.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiresMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}