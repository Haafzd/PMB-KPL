
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Models;
using PMB.Reporting;

using System;

namespace PMB.Tests
{
    [TestClass]
    public class ReportTest
    {
        [TestMethod]
        public void GenerateReport_WithValidDataAndHeader_ShouldIncludeHeaderRow()
        {
            // Arrange
            var data = new List<StudentReportData>
            {
                new StudentReportData { Name = "Fajar", NIM = "123", Major = "SE", Year = 2022 }
            };

            var template = new List<ReportColumnDefinition>
            {
                new ReportColumnDefinition { HeaderName = "Nama", DataPropertyName = "Name", Order = 1 },
                new ReportColumnDefinition { HeaderName = "NIM", DataPropertyName = "NIM", Order = 2 }
            };

            var config = new ReportFormatConfig
            {
                Separator = ",",
                UseHeader = true
            };

            var generator = new ReportGenerator(config);

            // Act
            string result = generator.GenerateReport(data, template);

            // Assert
            StringAssert.StartsWith(result, "Nama,NIM");
        }
    }
}
