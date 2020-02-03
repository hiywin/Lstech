using Lstech.Common.Data;
using Lstech.Entities.Health;
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


        /// <summary>
        /// 统计组员填写次数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealth_staff_Model>>> GetHealthStaffCount(QueryData<GetHealthStaffCountQuery> query);


    }
}
