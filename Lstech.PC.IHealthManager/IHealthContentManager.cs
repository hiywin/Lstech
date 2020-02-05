using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.IHealthManager
{
    public interface IHealthContentManager
    {
        Task<ListResult<IHealthContent>> GetHealthContentPageAsync(QueryData<HealthContentQuery> query);

        Task<ListResult<DataTable>> GetHealthContentPageAsyncEx(QueryData<HealthContentQuery> query);

        Task<ListResult<DataTable>> GetHealthContentAllAsync(QueryData<HealthContentQuery> query);

        Task<ErrData<byte[]>> HealthContentExcelExportAsync(QueryData<HealthContentQuery> query);

        Task<ErrData<byte[]>> HealthContentExcelExportAllAsync(QueryData<HealthContentQuery> query);

        Task<ListResult<DataTable>> GetHealthContentHrAllAsync(QueryData<HealthContentQuery> query);

        Task<ErrData<byte[]>> HealthContentExcelExportHrAllAsync(QueryData<HealthContentQuery> query);
    }
}
