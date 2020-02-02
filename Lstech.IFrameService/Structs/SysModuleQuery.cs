using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.IFrameService.Structs
{
    public struct SysModuleQuery
    {
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? StarTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string CreateName { get; set; }
    }
}
