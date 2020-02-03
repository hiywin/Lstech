using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.IHealthManager
{
    public interface IHealthAccountManager
    {
        Task<ErrData<IHealthUser>> HealthUserLoginAsync(QueryData<HealthUserQuery> query);
    }
}
