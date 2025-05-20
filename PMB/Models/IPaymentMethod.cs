using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Models
{
    public interface IPaymentMethod
    {
        bool ProcessPayment(decimal amount);
    }

}
