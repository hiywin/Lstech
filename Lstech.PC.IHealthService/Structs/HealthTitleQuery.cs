using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.PC.IHealthService.Structs
{
    public class HealthTitleQuery
    {
        public string Content { get; set; }
        public string Creator { get; set; }
        public bool? IsShow { get; set; }
        public string ParentId { get; set; }
        public bool IsParentQuery { get; set; }
    }
}
