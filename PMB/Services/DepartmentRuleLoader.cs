using PMB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Services
{

    public class DepartmentRuleLoader
    {
        public List<DepartmentRule> LoadRules()
        {
            return new List<DepartmentRule>
            {
                new DepartmentRule { DepartmentId = "CS", MinMathScore = 80, RequiredSchoolOrigin = "SMA" },
                new DepartmentRule { DepartmentId = "EE", MinMathScore = 75, RequiredSchoolOrigin = "SMK" },
                new DepartmentRule { DepartmentId = "CE", MinMathScore = 70, RequiredSchoolOrigin = "SMA" }
            };
        }
    }
}
