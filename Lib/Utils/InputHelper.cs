using System.Text.RegularExpressions;
namespace Lib.Utils
{
    public static class InputHelper
    {
        public static string Prompt(string label)
        {
            Console.Write(label);
            return Console.ReadLine() ?? "";
        }
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static string PromptRequired(string label)
        {
            string input;
            do
            {
                Console.Write(label);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("Input wajib diisi!");
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        public static string PromptPassword(string label)
        {
            Console.Write(label);
            return ReadPassword();
        }

        private static string ReadPassword()
        {
            var password = string.Empty;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
