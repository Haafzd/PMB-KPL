using System.Reflection;
using API.Models;
using API.Models.Reporting;
using API.Models.StateMachine;

namespace API.Services
{
    public class ReportGenerator
    {
        private readonly ReportStateMachine _stateMachine;

        public ReportGenerator()
        {
            _stateMachine = new ReportStateMachine();
        }

        public string GenerateReport(
            List<StudentReportData> data,
            List<ReportColumnDefinition> template,
            ReportFormatConfig config) 
        {
            _stateMachine.Fire(ReportTrigger.StartGeneration);

            if (data == null) throw new ArgumentNullException(nameof(data));
            if (template == null || !template.Any()) throw new ArgumentException("Template tidak boleh kosong.");
            if (config.Separator == null) throw new ArgumentException("Separator harus dikonfigurasi.");

            var props = typeof(StudentReportData).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var column in template)
            {
                if (!props.Any(p => p.Name == column.DataPropertyName))
                {
                    _stateMachine.Fire(ReportTrigger.ValidationFailed);
                    throw new ArgumentException($"Properti {column.DataPropertyName} tidak ditemukan.");
                }
            }

            _stateMachine.Fire(ReportTrigger.ValidationSuccess);

            var output = new List<string>();
            if (config.UseHeader)
            {
                output.Add(string.Join(config.Separator, template.Select(t => t.HeaderName)));
            }

            try
            {
                foreach (var item in data)
                {
                    var row = template.Select(t =>
                    {
                        var prop = props.First(p => p.Name == t.DataPropertyName);
                        return prop.GetValue(item)?.ToString() ?? "";
                    });
                    output.Add(string.Join(config.Separator, row));
                }

                _stateMachine.Fire(ReportTrigger.GenerationSuccess);
            }
            catch
            {
                _stateMachine.Fire(ReportTrigger.GenerationFailed);
                throw;
            }

            var result = string.Join(Environment.NewLine, output);
            if (string.IsNullOrWhiteSpace(result)) throw new InvalidOperationException("Output laporan tidak boleh kosong.");
            return result;
        }
    }
}