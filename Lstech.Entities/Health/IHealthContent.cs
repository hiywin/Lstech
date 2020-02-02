using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.Health
{
    public interface IHealthContent
    {
        int Id { get; set; }
        string ContentId { get; set; }
        string TitleId { get; set; }
        string Answer { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
    }
}
