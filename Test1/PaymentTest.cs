using API.Services;

namespace PMB.Tests
{
    [TestClass]
    public class PaymentTests
    {
        [TestMethod]
        public void CreditCardPayment_ProcessPayment_ValidAmount_ReturnsTrue()
        {
            var payment = new CreditCardPayment("1234567898765432");
            bool result = payment.ProcessPayment(100m);
            Assert.IsTrue(result);
        }
    }
}
