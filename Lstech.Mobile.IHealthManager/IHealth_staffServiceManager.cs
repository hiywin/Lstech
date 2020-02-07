using Lstech.Common.Data;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.IHealthManager
{
    /// <summary>
    /// 组织人员操作接口定义
    /// </summary>
    public interface IHealth_staffServiceManager
    {
        /// <summary>
        /// 根据工号获取人员信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<HealthStaff>> GetHealthStaffInfoAsync(QueryData<GetHealthStaffInfoQuery> query);
    }
}
