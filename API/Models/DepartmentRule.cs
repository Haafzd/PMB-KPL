namespace API.Models
{
    public class DepartmentRule
    {
        public string DepartmentId { get; set; }
        public Dictionary<string, int> RequiredSubjects { get; set; }
        public string RequiredSchoolType { get; set; }
        public decimal Fee { get; set; }
    }
}