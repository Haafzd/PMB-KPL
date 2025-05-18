using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Reporting
{
    public  class ReportTemplateLoader
    {
        public List<ReportColumnDefinition> LoadTemplate()
        {
            return new List<ReportColumnDefinition>
            {
                new ReportColumnDefinition { HeaderName = "Nama", DataPropertyName = "Name", Order = 1 },
                new ReportColumnDefinition { HeaderName = "Jurusan", DataPropertyName = "Major", Order = 2 },
                new ReportColumnDefinition { HeaderName = "Angkatan", DataPropertyName = "Year", Order = 3 }
            };
        }
    }
}
