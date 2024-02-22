namespace Shared.Models
{
    public class JobSetting
    {
        public const string ConfigKey = "JobSetting";
        public bool Active { get; set; }
        public string Frequency { get; set; } = null;
    }
}