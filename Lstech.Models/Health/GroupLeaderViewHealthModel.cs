using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    /// <summary>
    /// 组长查看
    /// </summary>
    public class GroupLeaderViewHealthModel
    {
        /// <summary>
        /// 未填写
        /// </summary>
        public int NotFilledCount { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 组员填写信息
        /// </summary>
        public List<Health_staff_Model> health_Staffs { get; set; }
    }
}
