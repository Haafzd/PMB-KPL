using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Models
{

    public class DepartmentRule
    {
        public string DepartmentId { get; set; }
        public int MinMathScore { get; set; }
        public string RequiredSchoolOrigin { get; set; }
    }
}
