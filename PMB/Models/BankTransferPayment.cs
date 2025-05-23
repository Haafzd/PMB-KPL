using PMB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Models
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
    }
}
