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
            return _quotaConfig.CourseQuotas.TryGetValue(departmentId, out var quota) && quota > 0;
        }
    }
}