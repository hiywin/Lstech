using Lstech.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    /// <summary>
    /// 组长查看组员填写（根据组员工号和姓名）
    /// </summary>
    public class TeamLeaderQueryTeamModel
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
        [Required]
        public PageModel PageModel { get; set; }
    }
}
