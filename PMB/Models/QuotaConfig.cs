using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Models
{
    public class QuotaConfig
    {
        public Dictionary<string, int> CourseQuotas { get; set; } = new();

        public void DecreaseQuota(string departmentId)
        {
            if (CourseQuotas.ContainsKey(departmentId))
            {
                CourseQuotas[departmentId]--;
            }
        }
    }
}