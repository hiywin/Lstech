using Lstech.Common.Data;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.IHealthManager
{
    public interface IHealth_contentManager
    {
        /// <summary>
        /// 提交提交内容详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> InsertHealthContentMaAsync(QueryData<Health_content_Model> query);

        /// <summary>
        /// 组长查看组员填写
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<List<Health_staff_Model>>> GetHealthStaffCountAsync(QueryData<GetHealthStaffCountQuery> query);
    }
}
