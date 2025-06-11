using API.Models;
using API.Services;
using Lib.Utils;

namespace PMB
{
    public class PMBMenu
    {
        private static async Task HandleRegister()
        {
            var email = InputHelper.PromptRequired("Email: ");
            if (!InputHelper.ValidateEmail(registration.Email)){
                Console.WriteLine("Format email tidak valid.");
                Console.ReadKey();
                return;
            }
            var password = InputHelper.PromptRequired("Password: ");
            var confirmPassword = InputHelper.PromptRequired("Ulangi Password: ");
            var result = await _client.RegisterAsync(email, password, confirmPassword);

            Console.WriteLine(result.IsSuccess ? "Registrasi berhasil!" : $"Gagal: {result.ErrorMessage}");
            Console.ReadKey();
        }
    }
}
