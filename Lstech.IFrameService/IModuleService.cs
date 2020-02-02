using Lstech.Common.Data;
using Lstech.Entities.Frame;
using Lstech.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.IFrameService
{
    public interface IModuleService
    {
        Task<DataResult<List<ISysModuleModel>>> GetModulesAllAsync(QueryData<SysModuleQuery> query);
    }
}
