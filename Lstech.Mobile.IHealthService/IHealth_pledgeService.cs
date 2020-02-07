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
    /// /承诺书确认接口定义
    /// </summary>
    public interface IHealth_pledgeService
    {
        /// <summary>
        /// 根据工号获取承诺书信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealth_pledge_Model>>> GetHealthPledgeInfo(QueryData<GetHealthPledgeInfoQuery> query);


        Task<DataResult<int>> InsertHealthPledgeInfo(QueryData<InsertHealthPledgeInfoQuery> query);
    }
}
