using Lib.Utils;

namespace API.Models
{
    public class StudentReportData
    {
        public string Name { get; set; }
        public string Major { get; set; }
        public int Year { get; set; }
        public string NIM { get; set; }
        public StudentReportData() { }
        public StudentReportData(string name, string major, int year)
        {
            Name = name;
            Major = major;
            Year = year;
            NIM = NimGenerator.GenerateNim();
        }
    }
}
