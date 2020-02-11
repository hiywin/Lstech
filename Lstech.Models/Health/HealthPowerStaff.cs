using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    public class HealthPowerStaff: IHealthPowerStaff
    {
        public string UserNo { get; set; }
        public string StaffName { get; set; }
    }
}
