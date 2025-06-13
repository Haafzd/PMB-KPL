using API.Models;
using System.Text.Json;

namespace API.Services
{
    public class UserService
    {
        private readonly string _filePath = Path.Combine("Data", "users.json");
        private readonly List<User> _users;

        public IEnumerable<User> Users => _users;

        public UserService()
        {
            _users = LoadUsers();
        }

        public void AddUser(User user)
        {
            _users.Add(user);
            SaveUsers();
        }

        private List<User> LoadUsers()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<User>();

                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat users.json: {ex.Message}");
                return new List<User>();
            }
        }

        private void SaveUsers()
        {
            try
            {
                var dir = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir!);

                var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan users.json: {ex.Message}");
            }
        }
    }
}