using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.PC.IHealthService.Structs
{
    public class HealthContentQuery
    {
        public string Answer { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public string CommondLeaderNo { get; set; }
        public DateTime? StarTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string HrLeaderNo { get; set; }
    }
}
