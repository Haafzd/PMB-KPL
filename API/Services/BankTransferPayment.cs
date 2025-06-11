using Lib.Utils;

namespace API.Services
{
    public class BankTransferPayment : IPaymentMethod
    {
        public string BankAccount { get; }

        public BankTransferPayment(string bankAccount)
        {
            if (string.IsNullOrWhiteSpace(bankAccount))
                throw new ArgumentException("bank ga bisa koksong.");

            BankAccount = bankAccount;
        }

        public bool ProcessPayment(decimal amount)
        {
            PaymentValidator.ValidateAmount(amount);

            // Simulasi proses transfer bank
            Console.WriteLine($"sedang memproses kepada bank dengan besaran {amount:C} kepada akun {BankAccount}.");
            return true; // misal pembayaran berhasil
        }

        public async Task<bool> ProcessPaymentAsync(string bankAccount, decimal amount)
        {
            await Task.Delay(1000); // Simulasi delay
            var paymentProcessor = new PaymentProcessor<IPaymentMethod>(this);
            var paymentExecute = paymentProcessor.ExecutePayment(amount);
            return true;
        }
    }
}
