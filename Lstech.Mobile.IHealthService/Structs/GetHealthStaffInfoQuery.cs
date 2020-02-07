using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Mobile.IHealthService.Structs
{
    /// <summary>
    ///根据工号获取组织人员信息
    /// </summary>
    public class GetHealthStaffInfoQuery
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
