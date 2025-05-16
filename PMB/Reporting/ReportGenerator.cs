using PMB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Reporting
{
    public class ReportGenerator
    {
        private readonly ReportFormatConfig _config;

        public ReportGenerator(ReportFormatConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public string GenerateReport(List<StudentReportData> data, List<ReportColumnDefinition> template)
        {
            // === PRECONDITIONS ===
            if (data == null) throw new ArgumentNullException(nameof(data), "Data tidak boleh null.");
            if (template == null || !template.Any()) throw new ArgumentException("Template tidak boleh kosong.");
            if (_config.Separator == null) throw new ArgumentException("Separator harus dikonfigurasi.");

            // Validasi setiap DataPropertyName ada di StudentReportData
            var props = typeof(StudentReportData).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var column in template)
            {
                if (!props.Any(p => p.Name == column.DataPropertyName))
                {
                    throw new ArgumentException($"Properti {column.DataPropertyName} tidak ditemukan dalam StudentReportData.");
                }
            }

            // === PROSES LAPORAN ===
            var output = new List<string>();

            if (_config.UseHeader)
            {
                var headers = template.Select(t => t.HeaderName);
                output.Add(string.Join(_config.Separator, headers));
            }

            foreach (var item in data)
            {
                var row = template.Select(t =>
                {
                    var prop = props.First(p => p.Name == t.DataPropertyName);
                    var value = prop.GetValue(item)?.ToString() ?? "";
                    return value;
                });

                output.Add(string.Join(_config.Separator, row));
            }

            var result = string.Join(Environment.NewLine, output);

            // === POSTCONDITION ===
            if (string.IsNullOrWhiteSpace(result)) throw new InvalidOperationException("Output laporan tidak boleh kosong.");

            return result;
        }
    }

}
