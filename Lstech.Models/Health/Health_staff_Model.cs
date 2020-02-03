using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    public class Health_staff_Model: IHealth_staff_Model
    {
        public string StaffNo { get; set; }
        public string STAFFName { get; set; }
        public string iswrite { get; set; }
    }
}
