using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.Health
{
    public interface IHealthTitle
    {
        int Id { get; set; }
        string TitleId { get; set; }
        string Content { get; set; }
        string Type { get; set; }
        bool? IsMustFill { get; set; }
        string ParentId { get; set; }
        string Creator { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        DateTime? UpdateTime { get; set; }
        int Sort { get; set; }
        bool? IsShow { get; set; }
    }
}
