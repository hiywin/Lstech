using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    public class HealthUserStaff: IHealthUserStaff
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string StaffNo { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
