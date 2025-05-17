using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;

using PMB.Models;
using PMB.Services;

class Program
{
    static void Main(string[] args)
    {

        try
        {
            var creditCardPayment = new CreditCardPayment("1234567812345678");
            var creditProcessor = new PaymentProcessor<CreditCardPayment>(creditCardPayment);
            creditProcessor.ExecutePayment(1000m);

            var bankTransferPayment = new BankTransferPayment("9876543210");
            var bankProcessor = new PaymentProcessor<BankTransferPayment>(bankTransferPayment);
            bankProcessor.ExecutePayment(500m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }
}
