using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    /// <summary>
    /// 返厂判定
    /// </summary>
    public class ReturnToFactoryJudgement
    {
        public ReturnToFactoryJudgement()
        {
            Start = "NG";
            Massage = "";
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// 返回内容
        /// </summary>
        public string Massage { get; set; }
    }
}
