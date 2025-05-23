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
        private readonly DepartmentRuleValidator _ruleValidator;

        public DepartmentController(
            DepartmentRuleLoader ruleLoader,
            DepartmentQuotaService quotaService,
            DepartmentRuleValidator ruleValidator)
        {
            _ruleLoader = ruleLoader;
            _quotaService = quotaService;
            _ruleValidator = ruleValidator;
        }

        // GET: Mengambil semua aturan jurusan
        [HttpGet("rules")]
        public ActionResult<List<DepartmentRule>> GetRules()
        {
            try
            {
                var rules = _ruleLoader.LoadRules();
                return Ok(rules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rules: {ex.Message}");
            }
        }

        // GET: Mengecek ketersediaan kuota jurusan
        [HttpGet("quota/{departmentId}")]
        public IActionResult CheckQuota(string departmentId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(departmentId))
                    return BadRequest("Kode jurusan harus diisi");

                var isAvailable = _quotaService.IsQuotaAvailable(departmentId);
                return Ok(new
                {
                    DepartmentId = departmentId.ToUpper(),
                    Available = isAvailable
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error checking quota: {ex.Message}");
            }
        }

        [HttpGet("quota-config")]
        public IActionResult GetQuotaConfig()
        {
            try
            {
                var config = _quotaService.GetCurrentQuotas();
                return Ok(config);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving quota config: {ex.Message}");
            }
        }
    }
}