﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Reporting
{
    public enum ReportState
    {
        NotStarted,
        Validating,
        Generating,
        Completed,
        Failed
    }

}
