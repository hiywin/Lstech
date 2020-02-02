using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    public class HealthContentSub: IHealthContentSub
    {
        public int Id { get; set; }
        public string ContentId { get; set; }
        public string TitleId { get; set; }
        public string Answer { get; set; }
    }
}
