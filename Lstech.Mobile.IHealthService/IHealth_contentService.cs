using Lstech.Common.Data;
using Lstech.Mobile.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.IHealthService
{
    /// <summary>
    /// 体检详细信息操作接口定义
    /// </summary>
    public interface IHealth_contentService
    {
        /// <summary>
        /// 提交体检详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> InsertHealthContent(QueryData<InsertHealthContentQuery> query);
    }
}
