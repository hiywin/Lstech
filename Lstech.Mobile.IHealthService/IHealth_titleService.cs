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
    /// 体检内容表操作接口定义
    /// </summary>
    public interface IHealth_titleService
    {
        Task<DataResult<List<IHealth_title_Model>>> GetAllHealthTitle(QueryData<GetAllHealthTitleQuery> query);
    }
}
