using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    /// <summary>
    ///承诺书验证
    /// </summary>
    public class Health_pledge_Model: IHealth_pledge_Model
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
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
        /// 签订时间
        /// </summary>
        public DateTime SignTime { get; set; }
        /// <summary>
        /// 承诺书类型：1：行政点检承诺书
        /// </summary>
        public string PledgeType { get; set; }
    }
}
