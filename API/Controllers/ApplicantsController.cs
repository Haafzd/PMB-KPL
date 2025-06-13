using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly IDepartmentService _departmentService;

        public ApplicantController(
            IApplicantService applicantService,
            IDepartmentService departmentService)
        {
            _applicantService = applicantService;
            _departmentService = departmentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Applicant>), 200)]
        public ActionResult<List<Applicant>> GetAll() => _applicantService.GetAllApplicants();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Applicant), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Applicant> GetById(int id)
        {
            var applicant = _applicantService.GetApplicantById(id);
            if (applicant == null)
                return NotFound();
            return Ok(applicant);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Applicant), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Create([FromBody] Applicant applicant)
        {
            var validationApplication = _departmentService.ValidateApplicant(applicant);
            var validationError = _departmentService.GetApplicantValidationReason(applicant);
            if (validationError != null)
                return BadRequest(validationError);

            var fee = _departmentService.GetFeeByDepartment(applicant.DepartmentId);
            if (fee == null || fee <= 0)
                return BadRequest("Biaya jurusan tidak ditemukan atau belum diatur.");

            var paymentMethod = PaymentFactory.CreatePaymentMethod(applicant.PaymentMethod, applicant.BankAccount);
            var processor = new PaymentProcessor<IPaymentMethod>(paymentMethod);

            bool paymentSuccess;
            try
            {
                paymentSuccess = await paymentMethod.ProcessPaymentAsync(applicant.BankAccount, fee.Value);
            }
            catch (Exception ex)
            {
                return BadRequest($"Pembayaran gagal: {ex.Message}");
            }

            _applicantService.AddApplicant(applicant);
            _departmentService.DecreaseQuota(applicant.DepartmentId);

            return CreatedAtAction(nameof(GetById), new { id = applicant.Id }, applicant);
        }
    }
}
