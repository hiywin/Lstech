using Lstech.Common.Data;
using Lstech.Models.Health;
using System;
using System.Threading.Tasks;

namespace Lstech.Mobile.IHealthManager
{
    public interface IHealth_titleManager
    {
        /// <summary>
        /// 获取全部体检内容表头接口定义
        /// </summary>
        /// <returns></returns>
        Task<ListResult<Health_title_Model>> GetHealthTitleAllAsync();
    }
}
