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
    }
}
