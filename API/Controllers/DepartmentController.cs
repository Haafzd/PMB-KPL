using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("rules")]
        public ActionResult<List<DepartmentRule>> GetRules() => _departmentService.GetRules();

        [HttpGet("quota")]
        public ActionResult<Dictionary<string, int>> GetQuotas() => _departmentService.GetQuotas();
    }
}