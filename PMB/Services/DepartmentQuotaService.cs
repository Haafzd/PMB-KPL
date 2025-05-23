using PMB.Models;
using System;

namespace PMB.Services
{
    /// Service untuk manajemen kuota jurusan
    public class DepartmentQuotaService
    {
        private readonly QuotaConfig _quotaConfig;

        public DepartmentQuotaService(QuotaConfig quotaConfig)
        {
            _quotaConfig = quotaConfig ?? throw new ArgumentNullException(nameof(quotaConfig));
        }

        /// Mengecek ketersediaan kuota untuk jurusan tertentu
        public bool IsQuotaAvailable(string departmentId)
        {
            return _quotaConfig.CourseQuotas.ContainsKey(departmentId) &&
                   _quotaConfig.CourseQuotas[departmentId] > 0;
        }

        public Dictionary<string, int> GetCurrentQuotas() =>
            new Dictionary<string, int>(_quotaConfig.CourseQuotas);
    }
}
