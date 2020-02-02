using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Common.Data
{
    public class QueryData<C>
    {
        public QueryData()
        {
            PageModel = new PageModel();
        }

        /// <summary>
        /// 查询条件结构
        /// </summary>
        public C Criteria { get; set; }

        /// <summary>
        /// 查询分页信息
        /// </summary>
        public PageModel PageModel { get; set; }

        /// <summary>
        /// 其他查询条件
        /// </summary>
        public QueryExtend Extend { get; set; }

    }
}
