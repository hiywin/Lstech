using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.IHealthService
{
    public interface IHealthAccountService
    {
        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthUser>>> GetHealthUserPageAsync(QueryData<HealthUserQuery> query);
    }
}
