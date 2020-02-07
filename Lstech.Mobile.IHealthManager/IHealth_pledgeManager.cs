using Lstech.Common.Data;
using Lstech.Mobile.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.IHealthManager
{
    public interface IHealth_pledgeManager
    {
        /// <summary>
        /// 根据工号验证是否确认承诺书
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> GetHealthPledgeByNoMaAsync(QueryData<GetHealthPledgeInfoQuery> query);

        /// <summary>
        /// 保存确认承诺书
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> InsertHealthPledgeInfoMaAsync(QueryData<InsertHealthPledgeInfoQuery> query);
    }
}
