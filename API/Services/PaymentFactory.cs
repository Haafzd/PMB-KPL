namespace API.Services
{
    public static class PaymentFactory
    {
        public static IPaymentMethod CreatePaymentMethod(string method, string account)
        {
            return method.ToLower() switch
            {
                "bank" => new BankTransferPayment(account),
                "credit" => new CreditCardPayment(account),
                // Tambahkan metode baru di sini
                _ => throw new ArgumentException("Metode pembayaran tidak dikenal.")
            };
        }
    }
}
