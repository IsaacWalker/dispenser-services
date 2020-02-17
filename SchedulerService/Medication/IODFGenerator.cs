﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;
using Web.PrinterClient;

namespace Web.SchedulerService.Medication
{
    public interface IODFGenerator
    {
        ODF Run(Prescription prescription);
    }
}
