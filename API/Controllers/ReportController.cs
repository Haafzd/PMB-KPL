using Microsoft.AspNetCore.Mvc;
using API.Models.Reporting;
using API.Services;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportGenerator _reportGenerator;

        public ReportController(ReportGenerator reportGenerator)
        {
            _reportGenerator = reportGenerator;
        }

        [HttpPost("generate")]
        public ActionResult<string> GenerateReport([FromBody] List<StudentReportData> data)
        {
            try
            {
                var template = ReportTemplateLoader.LoadTemplate();
                var config = new ReportFormatConfig
                {
                    Separator = ",",
                    UseHeader = true
                };

                // PERBAIKAN 1: Teruskan config ke generator
                return _reportGenerator.GenerateReport(data, template, config);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
