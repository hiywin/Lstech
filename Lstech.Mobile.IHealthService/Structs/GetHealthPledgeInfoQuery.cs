using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Mobile.IHealthService.Structs
{
    /// <summary>
    ///获取承诺书确认信息
    /// </summary>
    public class GetHealthPledgeInfoQuery
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string StaffNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string StaffName { get; set; }
    }
}
