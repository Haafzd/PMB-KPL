namespace API.Services
{
    public class PaymentProcessor<T> where T : IPaymentMethod
    {
        private readonly T _paymentMethod;

        public PaymentProcessor(T paymentMethod)
        {
            _paymentMethod = paymentMethod ?? throw new ArgumentNullException("Metode pembayaran harus diisi");
        }

        // Eksekusi pembayaran dengan validasi amount  
        public bool ExecutePayment(decimal amount)
        {
            if (amount == 0) throw new ArgumentException("Tidak boleh kosong");

            if (amount <= 0) throw new ArgumentException("Jumlah harus lebih besar dari 0");

            bool result = _paymentMethod.ProcessPayment(amount);

            if (!result) throw new InvalidOperationException("Pembayaran gagal");
            return result;
        }
    }
}
