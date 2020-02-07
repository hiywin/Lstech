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
    /// 组织结构人员信息操作接口定义
    /// </summary>
    public interface IHealth_staffService
    {
        /// <summary>
        /// 根据工号获取组织人员信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthStaff>>> GetHealthStaffInfo(QueryData<GetHealthStaffInfoQuery> query);
    }
}
