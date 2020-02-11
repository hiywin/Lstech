using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Mobile.IHealthService.Structs
{
    /// <summary>
    /// 组长查看查询组员填写（根据组员工号姓名）
    /// </summary>
    public class GetTeamLeaderQueryModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 组长工号
        /// </summary>
        public string userNo { get; set; }
        /// <summary>
        /// 组员工号
        /// </summary>
        public string teamNO { get; set; }
        /// <summary>
        /// 组员姓名
        /// </summary>
        public string teamName { get; set; }
    }
}
