using API.Models;
using API.Controllers;
using System.Text.Json;
using System.Text;

namespace API.Services
{
    public class PMBClient
    {
        private readonly HttpClient _client;
        
        public PMBClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7229/api/");
        }

        #region Applicant Endpoints
        public async Task<List<Applicant>> GetApplicantsAsync()
        {
            try
            {
                var response = await _client.GetAsync("applicant");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<Applicant>>() ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching applicants: {ex.Message}");
                return new();
            }
        }

        public async Task<List<Applicant>> GetApplicantsByIdAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"applicant/{id}");
                response.EnsureSuccessStatusCode();

                var applicant = await response.Content.ReadFromJsonAsync<Applicant>();
                if (applicant == null)
                {
                    Console.WriteLine("Tidak ada applicant dengan ID " + id);
                    return new List<Applicant>();
                }

                return new List<Applicant> { applicant };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal mengambil data applicant: {ex.Message}");
                return new List<Applicant>();
            }
        }

        public async Task<bool> RegisterApplicantAsync(Applicant applicant)
        {
            try
            {
                var rules = await GetDepartmentRulesAsync();
                var selectedRule = rules.FirstOrDefault(r => r.DepartmentId == applicant.DepartmentId);

                if (selectedRule == null)
                {
                    Console.WriteLine("Jurusan tidak ditemukan.");
                    return false;
                }

                decimal registrationFee = selectedRule.Fee;

                var response = await _client.PostAsJsonAsync("applicant", applicant);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Pendaftaran gagal: {content}");
                    return false;
                }

                var paymentRequest = new PaymentRequest
                {
                    Method = applicant.PaymentMethod,
                    Account = applicant.BankAccount,
                    Amount = registrationFee
                };

                var paymentResponse = await ProcessPaymentAsync(paymentRequest);
                if (!paymentResponse)
                {
                    Console.WriteLine("Pendaftaran berhasil, tetapi pembayaran gagal diproses.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat mendaftar: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Department Endpoints
        public async Task<List<DepartmentRule>> GetDepartmentRulesAsync()
        {
            try
            {
                var response = await _client.GetAsync("department/rules");
                response.EnsureSuccessStatusCode();

                var rules = await response.Content.ReadFromJsonAsync<List<DepartmentRule>>();
                return rules ?? new List<DepartmentRule>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal mengambil data jurusan: {ex.Message}");
                return new List<DepartmentRule>();
            }
        }

        public async Task<Dictionary<string, int>> GetQuotasAsync()
        {
            try
            {
                var quotas = await _client.GetFromJsonAsync<Dictionary<string, int>>("department/quota");
                return quotas ?? new Dictionary<string, int>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting quotas: {ex.Message}");
                return new Dictionary<string, int>();
            }
        }

        public async Task<bool> DecreaseQuotaAsync(string departmentId)
        {
            if (string.IsNullOrEmpty(departmentId))
            {
                Console.WriteLine("ID jurusan tidak boleh kosong.");
                return false;
            }

            try
            {
                var response = await _client.PostAsJsonAsync<string>($"department/decrease-quota/{departmentId}", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal mengurangi kuota: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Payment Endpoints
        public async Task<bool> ProcessPaymentAsync(PaymentRequest request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("payment/process", request);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Gagal memproses pembayaran: {content}");
                    return false;
                }

                Console.WriteLine("Pembayaran berhasil diproses.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat memproses pembayaran: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Report Endpoints
       public async Task<string> GenerateReportAsync(List<StudentReportData> data)
        {
            try
            {
                if (data == null || !data.Any())
                    return "Tidak ada data untuk laporan.";

                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                    WriteIndented = false
                });

                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("report/generate", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Server error ({response.StatusCode}): {errorContent}";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return $"Kesalahan permintaan: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Laporan gagal dibuat: {ex.Message}";
            }
        }
        #endregion

        #region Authentication Endpoints
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("auth/login", 
                    new { Email = email, Password = password });
                
                return await HandleAuthResponse(response);
            }
            catch (Exception ex)
            {
                return new AuthResult { 
                    IsSuccess = false, 
                    ErrorMessage = ex.Message 
                };
            }
        }
        public async Task<AuthResult> RegisterAsync(string email, string password, string confirm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirm))
                {
                    return new AuthResult
                    {
                        IsSuccess = false,
                        ErrorMessage = "Email, password, dan konfirmasi tidak boleh kosong."
                    };
                }

                if (password != confirm)
                {
                    return new AuthResult
                    {
                        IsSuccess = false,
                        ErrorMessage = "Password dan konfirmasi tidak cocok."
                    };
                }

                var payload = new
                {
                    Email = email,
                    NewPassword = password,
                    ReEnterPassword = confirm
                };

                var response = await _client.PostAsJsonAsync("registration", payload);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new AuthResult
                    {
                        IsSuccess = false,
                        ErrorMessage = string.IsNullOrWhiteSpace(content)
                            ? $"Status: {response.StatusCode} (no message)"
                            : content
                    };
                }

                return new AuthResult
                {
                    IsSuccess = true,
                    ErrorMessage = content
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    ErrorMessage = $"Exception: {ex.Message}"
                };
            }
        }

        #endregion

        #region Helper Methods
        public async Task<AuthResult> HandleAuthResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            return new AuthResult
            {
                IsSuccess = response.IsSuccessStatusCode,
                ErrorMessage = string.IsNullOrWhiteSpace(content)
                    ? $"Status: {response.StatusCode} (no message)"
                    : content
            };
        }
        #endregion
    }
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}