using Microsoft.AspNetCore.Mvc;
using PMB.Models;
using PMB.Services;
using System.Linq;

namespace API
{

    // PMB-API/Controllers/PaymentController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        // POST: api/payment/bank-transfer
        [HttpPost("bank-transfer")]
        public IActionResult ProcessBankTransfer([FromBody] BankTransferPaymentRequest request)
        {
            try
            {
                var paymentMethod = new BankTransferPayment(request.BankAccount);
                var processor = new PaymentProcessor<BankTransferPayment>(paymentMethod);
                var result = processor.ExecutePayment(request.Amount);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class BankTransferPaymentRequest
    {
        public string BankAccount { get; set; }
        public decimal Amount { get; set; }
    }
}