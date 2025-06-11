namespace Lib.Utils
{
    public static class PaymentValidator
    {
        public static void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("harus lebih besar dari 0.");
        }

        public static bool IsValidPaymentFormat(string paymentNumber)
        {
            return paymentNumber != null && paymentNumber.Length == 16 && paymentNumber.All(char.IsDigit);
        }
    }
}
