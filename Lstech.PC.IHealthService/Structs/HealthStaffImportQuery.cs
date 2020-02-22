using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.PC.IHealthService.Structs
{
    public class HealthStaffImportQuery
    {
        public List<IHealthStaff> LstStaff { get; set; }
        public List<IHealthUserStaff> LstGroupStaff { get; set; }
        public List<IHealthUserStaff> LstAggStaff { get; set; }
        public List<IHealthUserStaff> LstHrStaff { get; set; }
    }
}
