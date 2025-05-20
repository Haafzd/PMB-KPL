using PMB.Models;
using System.Reflection;
using PMB.StateMachine;

namespace PMB.Reporting
{
    public class ReportGenerator
    {
        private readonly ReportFormatConfig _config;
        private readonly ReportStateMachine _stateMachine;

        public ReportGenerator(ReportFormatConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _stateMachine = new ReportStateMachine();
        }

        public string GenerateReport(List<StudentReportData> data, List<ReportColumnDefinition> template)
        {
            _stateMachine.Fire(ReportTrigger.StartGeneration);

            if (data == null) throw new ArgumentNullException(nameof(data));
            if (template == null || !template.Any()) throw new ArgumentException("Template tidak boleh kosong.");
            if (_config.Separator == null) throw new ArgumentException("Separator harus dikonfigurasi.");

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
            if (_config.UseHeader)
            {
                output.Add(string.Join(_config.Separator, template.Select(t => t.HeaderName)));
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
                    output.Add(string.Join(_config.Separator, row));
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
