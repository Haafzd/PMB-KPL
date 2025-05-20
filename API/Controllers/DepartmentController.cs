using Microsoft.AspNetCore.Mvc;
using PMB.Models;
using PMB.Services;
using System.Collections.Generic;
using System.Linq;

namespace API
{
    /// Controller untuk manajemen data jurusan
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRuleLoader _ruleLoader;
        private readonly DepartmentQuotaService _quotaService;

        public DepartmentController(
            DepartmentRuleLoader ruleLoader,
            DepartmentQuotaService quotaService)
        {
            _ruleLoader = ruleLoader;
            _quotaService = quotaService;
        }

        // GET: Mengambil semua aturan jurusan
        [HttpGet("rules")]
        public ActionResult<List<DepartmentRule>> GetRules()
        {
            return _ruleLoader.LoadRules();
        }

        // GET: Mengecek ketersediaan kuota jurusan
        [HttpGet("quota/{departmentId}")]
        public ActionResult<bool> CheckQuota(string departmentId)
        {
            return _quotaService.IsQuotaAvailable(departmentId);
        }
    }
}