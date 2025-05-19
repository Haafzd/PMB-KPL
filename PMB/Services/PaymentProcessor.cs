using PMB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Services
{

    public class PaymentProcessor<T> where T : IPaymentMethod
    {
        private readonly T _paymentMethod;

        public PaymentProcessor(T paymentMethod)
        {
            _paymentMethod = paymentMethod ?? throw new ArgumentNullException(nameof(paymentMethod));
        }

        public bool ExecutePayment(decimal amount)
        {
            // Precondition
            if (amount <= 0) throw new ArgumentException("harus lehih besar dari 0.");

            bool result = _paymentMethod.ProcessPayment(amount);

            // Postcondition
            if (!result)
                throw new InvalidOperationException("Pembayarn gagal.");

            return result;
        }
    }

}
