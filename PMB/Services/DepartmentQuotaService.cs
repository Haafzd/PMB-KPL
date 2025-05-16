using PMB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Services
{

    public class DepartmentQuotaService
    {
        private readonly QuotaConfig _quotaConfig;

        public DepartmentQuotaService(QuotaConfig quotaConfig)
        {
            _quotaConfig = quotaConfig;
        }

        public bool IsQuotaAvailable(string departmentId)
        {
            return _quotaConfig.CourseQuotas.ContainsKey(departmentId) &&
                   _quotaConfig.CourseQuotas[departmentId] > 0;
        }
    }
}
