namespace API.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchoolOrigin { get; set; }
        public Dictionary<string, int> SubjectScores { get; set; } = new();
        public string DepartmentId { get; set; }
        public string BankAccount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
