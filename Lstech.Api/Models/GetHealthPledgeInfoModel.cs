using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    public class GetHealthPledgeInfoModel
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
