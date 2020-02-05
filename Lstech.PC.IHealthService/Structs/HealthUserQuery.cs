using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.PC.IHealthService.Structs
{
    public class HealthUserQuery
    {
        public string AdAccount { get; set; }
        public string Pwd { get; set; }
        public bool IsAdmin { get; set; }
    }
}
