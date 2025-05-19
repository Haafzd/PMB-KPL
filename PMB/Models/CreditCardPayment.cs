using PMB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Models

{
    
    public class CreditCardPayment : IPaymentMethod
    {
        private string cardNumber;
        public string CardNumber { get; }

        public CreditCardPayment(string cardNumber)
        {
            if (!PaymentValidator.IsValidCreditCardFormat(cardNumber))
                throw new ArgumentException("format kredit card salah.");

            CardNumber = cardNumber;
        }

        public bool ProcessPayment(decimal amount)
        {
            PaymentValidator.ValidateAmount(amount);

            // Simulasi proses pembayaran dengan kartu kredit
            Console.WriteLine($"Pembayaran dengan nominal  {amount:C} menggunakan kartu ini" +
                $" {CardNumber}.");
            return true; // misal pembayaran berhasil
        }
    }

}
