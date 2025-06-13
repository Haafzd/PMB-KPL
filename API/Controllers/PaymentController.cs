using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Method) || string.IsNullOrWhiteSpace(request.Account))
                return BadRequest("Metode dan akun pembayaran wajib diisi.");

            if (request.Amount <= 0)
                return BadRequest("Jumlah pembayaran harus lebih dari 0.");

            try
            {
                var paymentMethod = PaymentFactory.CreatePaymentMethod(request.Method, request.Account);
                var success = await paymentMethod.ProcessPaymentAsync(request.Account, request.Amount);

                if (success)
                {
                    Console.WriteLine($"[INFO] Pembayaran berhasil dengan metode {request.Method} ke akun {request.Account} sebesar {request.Amount:C}.");
                    return Ok(new { Berhasil = true, Pesan = "Pembayaran berhasil." });
                }

                return BadRequest("Pembayaran gagal diproses.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Terjadi kesalahan: {ex.Message}");
            }
        }
    }

    public class PaymentRequest
    {
        public string Method { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
    }
}
