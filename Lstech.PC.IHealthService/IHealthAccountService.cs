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
        /// 创建新实例
        /// </summary>
        /// <returns></returns>
        IHealthUserStaff NewHealthUserStaff();

        /// <summary>
        /// 创建新实例
        /// </summary>
        /// <returns></returns>
        IHealthStaff NewHealthStaff();

        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthUser>>> GetHealthUserPageAsync(QueryData<HealthUserQuery> query);

        /// <summary>
        /// 分页获取人员结构列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthStaff>>> GetHealthStaffPageAsync(QueryData<HealthStaffQuery> query);

        /// <summary>
        /// 人员结构新增或修改
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthStaffSaveOrUpdateAsync(QueryData<HealthStaffQuery> query);

        /// <summary>
        /// 人员结构删除员工
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthStaffDeleteAsync(QueryData<HealthStaffQuery> query);

        /// <summary>
        /// 分页获取关联表-人员结构列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthUserStaffInfo>>> GetHealthUserStaffInfoPageAsync(QueryData<HealthUserStaffInfoQuery> query);

        /// <summary>
        /// 保存登录用户-员工关联表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthUserStaffSaveAsync(QueryData<HealthUserStaffSaveQuery> query);

        /// <summary>
        /// 删除登录用户-员工关联表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthUserStaffDeleteAsync(QueryData<HealthUserStaffDeleteQuery> query);

        /// <summary>
        /// 获取有权限管理用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IHealthPowerStaff>>> GetHealthPowerStaffPageAsync(QueryData<HealthPowerStaffQuery> query);

        /// <summary>
        /// 根据Excel模板导入员工数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> HealthStaffImportAsync(QueryData<HealthStaffImportQuery> query);

    }
}
