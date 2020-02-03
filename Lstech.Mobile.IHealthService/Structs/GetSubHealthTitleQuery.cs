using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Mobile.IHealthService.Structs
{
    /// <summary>
    /// 获取标题子内容
    /// </summary>
    public class GetSubHealthTitleQuery
    {
        public int id { get; set; }
        public string ParentId { get; set; }
        public bool? IsShow { get; set; }
    }
}
