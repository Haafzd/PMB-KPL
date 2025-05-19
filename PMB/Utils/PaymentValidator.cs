using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Utils
{
    public static class PaymentValidator
    {
        public static void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("harus lebih besar dari 0.");
        }

        public static bool IsValidCreditCardFormat(string cardNumber)
        {

            return cardNumber != null && cardNumber.Length == 16 && cardNumber.All(char.IsDigit);
        }

    }


}
