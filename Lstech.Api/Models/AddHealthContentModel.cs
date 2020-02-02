using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    public class AddHealthContentModel
    {
        /// <summary>
        /// 内容guid
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// health_title的TitleId
        /// </summary>
        public string TitleId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string TitleType { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 填写人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 填写时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateName { get; set; }
    }
}
