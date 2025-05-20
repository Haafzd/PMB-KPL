using Microsoft.AspNetCore.Mvc;
using PMB.Models;
using PMB.Reporting;
using System.Linq;

namespace API 
{

    // PMB-API/Controllers/ReportController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportGenerator _reportGenerator;

        // Remove templateLoader from constructor
        public ReportController(ReportGenerator reportGenerator)
        {
            _reportGenerator = reportGenerator;
        }

        [HttpPost("generate")]
        public ActionResult<string> GenerateReport([FromBody] List<StudentReportData> data)
        {
            try
            {
                // Call static method directly
                var template = ReportTemplateLoader.LoadTemplate();
                var config = new ReportFormatConfig
                {
                    Separator = ",",
                    UseHeader = true
                };
                return _reportGenerator.GenerateReport(data, template);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}