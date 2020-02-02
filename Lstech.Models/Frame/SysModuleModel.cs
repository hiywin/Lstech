using Lstech.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Frame
{
    public class SysModuleModel : ISysModuleModel
    {
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Category { get; set; }
        public string Target { get; set; }
        public int IsResource { get; set; }
        public int App { get; set; }
        public int Id { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
        public int Sort { get; set; }
    }
}
