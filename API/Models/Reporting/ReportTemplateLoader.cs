namespace API.Models.Reporting
{
    public static class ReportTemplateLoader
    {
        public static List<ReportColumnDefinition> LoadTemplate()
        {
            return new List<ReportColumnDefinition>
            {
                new ReportColumnDefinition { HeaderName = "Nama", DataPropertyName = "Name", Order = 1 },
                new ReportColumnDefinition { HeaderName = "Jurusan", DataPropertyName = "Major", Order = 2 },
                new ReportColumnDefinition { HeaderName = "Angkatan", DataPropertyName = "Year", Order = 3 },
                new ReportColumnDefinition { HeaderName = "NIM", DataPropertyName = "NIM", Order = 4 }
            };
        }
    }
}
