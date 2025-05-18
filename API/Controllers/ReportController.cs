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
        private readonly ReportTemplateLoader _templateLoader;

        public ReportController(ReportGenerator reportGenerator, ReportTemplateLoader templateLoader)
        {
            _reportGenerator = reportGenerator;
            _templateLoader = templateLoader;
        }

        // POST: api/report/generate
        [HttpPost("generate")]
        public ActionResult<string> GenerateReport([FromBody] List<StudentReportData> data)
        {
            try
            {
                var template = _templateLoader.LoadTemplate();
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