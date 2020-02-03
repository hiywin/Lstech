using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.Models.Health;
using Lstech.PC.IHealthService.Structs;
using System.Threading.Tasks;

namespace Lstech.PC.IHealthManager
{
    public interface IHealthTitleManager
    {
        Task<ListResult<HealthTitle>> GetHealthTitlePageAsync(QueryData<HealthTitleQuery> query);

        Task<ErrData<bool>> HealthTitleSaveAsync(QueryData<HealthTitleSaveQuery> query);

        Task<ErrData<bool>> HealthTitleUpdateAsync(QueryData<HealthTitleUpdateQuery> query);

        Task<ErrData<bool>> HealthTitleDeleteAsync(QueryData<HealthTitleDeleteQuery> query);
    }
}
