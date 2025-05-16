using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Reporting
{
    public class ReportColumnDefinition
    {
        public string HeaderName { get; set; }
        public string DataPropertyName { get; set; }
        public int Order { get; set; }
    }
}
