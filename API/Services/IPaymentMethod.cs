namespace API.Services
{
    public interface IPaymentMethod
    {
        bool ProcessPayment(decimal amount);
        Task<bool> ProcessPaymentAsync(string bankAccount, decimal amount);
    }
}
