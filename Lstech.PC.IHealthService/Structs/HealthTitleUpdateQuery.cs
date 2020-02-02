using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.PC.IHealthService.Structs
{
    public class HealthTitleUpdateQuery
    {
        public string TitleId { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public bool? IsMustFill { get; set; }
        public string ParentId { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int Sort { get; set; }
        public bool? IsShow { get; set; }
    }
}
