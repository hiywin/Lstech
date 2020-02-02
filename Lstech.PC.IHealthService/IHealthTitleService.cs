using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.IHealthService
{
    public interface IHealthTitleService
    {
        /// <summary>
        /// 获取所有标题
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthTitle>>> GetHealthTitleAllAsync(QueryData<HealthTitleQuery> query);

        /// <summary>
        /// 分页获取标题
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthTitle>>> GetHealthTitlePageAsync(QueryData<HealthTitleQuery> query);

        /// <summary>
        /// 添加标题
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthTitleSaveAsync(QueryData<HealthTitleSaveQuery> query);

        /// <summary>
        /// 修改标题
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthTitleUpdateAsync(QueryData<HealthTitleUpdateQuery> query);

        /// <summary>
        /// 删除标题
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthTitleDeleteAsync(QueryData<HealthTitleDeleteQuery> query);
    }
}
