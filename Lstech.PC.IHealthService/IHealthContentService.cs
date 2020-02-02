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
        /// 查询内容主表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthContentMain>>> GetHealthContentMainPageAsync(QueryData<HealthContentMainQuery> query);

        /// <summary>
        /// 查询内容子表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthContentSub>>> GetHealthContentSubAllAsync(QueryData<HealthContentSubQuery> query);

    }
}
