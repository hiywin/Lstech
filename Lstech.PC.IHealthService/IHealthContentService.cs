using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.IHealthService
{
    public interface IHealthContentService
    {
        /// <summary>
        /// 分页获取内容
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthContent>>> GetHealthContentPageAsync(QueryData<HealthContentQuery> query);

        /// <summary>
        /// 分页获取内容 - 关联人员结构表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthContentStaff>>> GetHealthContentStaffPageAsync(QueryData<HealthContentQuery> query);

        /// <summary>
        /// 获取所有内容
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthContentStaff>>> GetHealthContentStaffAllAsync(QueryData<HealthContentQuery> query);

        /// <summary>
        /// 获取部门总指挥
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthStaff>>> GetHealthStaffCommandAllAsync(QueryData<HealthStaffQuery> query);

    }
}
