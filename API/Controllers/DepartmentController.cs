using Microsoft.AspNetCore.Mvc;
using PMB.Models;
using PMB.Services;
using System.Linq;

namespace API
{

    // PMB-API/Controllers/DepartmentController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRuleLoader _ruleLoader;
        private readonly DepartmentQuotaService _quotaService;

        public DepartmentController(DepartmentRuleLoader ruleLoader, DepartmentQuotaService quotaService)
        {
            _ruleLoader = ruleLoader;
            _quotaService = quotaService;
        }

        // GET: api/department/rules
        [HttpGet("rules")]
        public ActionResult<List<DepartmentRule>> GetRules()
        {
            return _ruleLoader.LoadRules();
        }

        // GET: api/department/quota/{departmentId}
        [HttpGet("quota/{departmentId}")]
        public ActionResult<bool> CheckQuota(string departmentId)
        {
            return _quotaService.IsQuotaAvailable(departmentId);
        }
    }
}
