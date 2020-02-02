using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.Health
{
    public interface IHealthContentSub
    {
        int Id { get; set; }
        string ContentId { get; set; }
        string TitleId { get; set; }
        string Answer { get; set; }
    }
}
