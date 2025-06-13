using Lib.Utils;

namespace API.Services
{
    public class CreditCardPayment : IPaymentMethod
    {
        public string CardNumber { get; }

        public CreditCardPayment(string cardNumber)
        {
            if (!PaymentValidator.IsValidPaymentFormat(cardNumber))
                throw new ArgumentException("format kredit card salah.");

            CardNumber = cardNumber;
        }

        public bool ProcessPayment(decimal amount)
        {
            PaymentValidator.ValidateAmount(amount);


            Console.WriteLine($"Pembayaran dengan nominal  {amount:C} menggunakan kartu ini" +
                $" {CardNumber}.");

            return true;
        }

        public async Task<bool> ProcessPaymentAsync(string cardNumber, decimal amount)
        {
            await Task.Delay(1000);

            var paymentProcessor = new PaymentProcessor<IPaymentMethod>(this);
            var paymentExecute = paymentProcessor.ExecutePayment(amount);

            return true;
        }
    }
}
