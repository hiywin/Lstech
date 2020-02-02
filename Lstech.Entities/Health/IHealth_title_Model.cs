using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.Health
{
    /// <summary>
    /// 体检表内容表
    /// </summary>
    public interface IHealth_title_Model
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题Id
        /// </summary>
        public string TitleId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 类型：1.标题，2.文本，3.单选，4.复选'
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public int IsMustFill { get; set; }
        /// <summary>
        /// 父编号(根据TitleId)
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Updator { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}
