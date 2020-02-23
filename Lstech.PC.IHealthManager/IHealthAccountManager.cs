using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthManager.Structs;
using Lstech.PC.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.IHealthManager
{
    public interface IHealthAccountManager
    {
        Task<ErrData<IHealthUser>> HealthUserLoginAsync(QueryData<HealthUserQuery> query);

        Task<ErrData<IHealthUser>> HealthUserLoginExAsync(QueryData<HealthUserQuery> query);

        Task<ListResult<IHealthUser>> HealthUsersPageAsync(QueryData<HealthUserQuery> query);

        Task<ListResult<IHealthStaff>> HealthStaffsPageAsync(QueryData<HealthStaffQuery> query);

        Task<ErrData<bool>> HealthStaffSaveOrUpdateAsync(QueryData<HealthStaffQuery> query);

        Task<ErrData<bool>> HealthStaffDeleteAsync(QueryData<HealthStaffQuery> query);

        Task<ErrData<bool>> HealthStaffBatchDeleteAsync(QueryData<HealthStaffBatchDeleteQuery> query);

        Task<ErrData<bool>> HealthUserStaffSaveAsync(QueryData<HealthUserStaffModel> query);

        Task<ListResult<IHealthUserStaffInfo>> HealthUserStaffInfoPageAsync(QueryData<HealthUserStaffInfoQuery> query);

        Task<ErrData<bool>> HealthUserStaffDeleteAsync(QueryData<HealthUserStaffDeleteQuery> query);

        Task<ListResult<IHealthPowerStaff>> HealthPowerStaffPageAsync(QueryData<HealthPowerStaffQuery> query);

        Task<ErrData<bool>> HealthStaffImportAsync(QueryData<Stream> query);
    }
}
