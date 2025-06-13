namespace API.Models
{
    public class QuotaConfig
    {
        public Dictionary<string, int> CourseQuotas { get; set; } = new();

        public void DecreaseQuota(string departmentId)
        {
            if (CourseQuotas.ContainsKey(departmentId) && CourseQuotas[departmentId] > 0)
                CourseQuotas[departmentId]--;
        }
    }
}