namespace API.Models
{
    public class QuotaConfig
    {
        public Dictionary<string, int> CourseQuotas { get; set; } = new();
    }
}