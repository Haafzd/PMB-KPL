using PMB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Services
{
    public class DepartmentRuleValidator
    {
        private readonly List<DepartmentRule> _rules;

        public DepartmentRuleValidator(List<DepartmentRule> rules)
        {
            _rules = rules;
        }
        //dbc nya
        public bool IsValid(Applicant applicant, string departmentId)
        {
            var rule = _rules.FirstOrDefault(r => r.DepartmentId == departmentId);
            if (rule == null) return false;

            return applicant.MathScore >= rule.MinMathScore &&
                   applicant.SchoolOrigin == rule.RequiredSchoolOrigin;
        }
    }
}
