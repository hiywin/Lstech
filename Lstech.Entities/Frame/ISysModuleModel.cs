using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.Frame
{
    public interface ISysModuleModel
    {
        string ModuleNo { get; set; }
        string ModuleName { get; set; }
        string ParentNo { get; set; }
        string Icon { get; set; }
        string Url { get; set; }
        int Category { get; set; }
        string Target { get; set; }
        int IsResource { get; set; }
        int App { get; set; }
        int Sort { get; set; }
        int Id { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
    }
}
