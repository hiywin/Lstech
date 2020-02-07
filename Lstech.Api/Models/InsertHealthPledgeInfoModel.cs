using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    public class InsertHealthPledgeInfoModel
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string StaffNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string StaffName { get; set; }
        /// <summary>
        /// 是否签订：true:已签,false:未签
        /// </summary>
        public bool IsSign { get; set; }
        /// <summary>
        /// 承诺书类型：1：行政点检承诺书
        /// </summary>
        public string PledgeType { get; set; }
    }
}
