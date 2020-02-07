using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Common.Data
{
    public class QueryExtend
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string ConnString { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        /// 登录人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 入库类型：原材料入库、退料入库、原材料出库
        /// </summary>
        public string StorageType { get; set; }

        /// <summary>
        /// 登录仓库
        /// </summary>
        public string RepertoryId { get; set; }

        /// <summary>
        /// EAS权限
        /// </summary>
        public string UnitFid { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
