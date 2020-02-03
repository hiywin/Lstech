using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    public class HealthUser: IHealthUser
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string AdAccount { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
