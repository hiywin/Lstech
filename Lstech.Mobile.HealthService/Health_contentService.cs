using Lstech.Common.Data;
using Lstech.Common.Helpers;
using Lstech.Entities.Health;
using Lstech.Mobile.IHealthService;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthService
{
    /// <summary>
    /// 体检详细信息提交数据接口实现
    /// </summary>
    public class Health_contentService : IHealth_contentService
    {
        /// <summary>
        /// 组长查看组员填写
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_staff_Model>>> GetHealthStaffCount(QueryData<GetHealthStaffCountQuery> query)
        {
            var lr = new DataResult<List<IHealth_staff_Model>>();

            string sql = string.Format(@"select staff.StaffNo,staff.STAFFName,case   when content.Contentid is not null then 1 when content.Contentid is null then 0 else 0 end iswrite from health_staff staff left join health_content content on content.Creator=staff.StaffNo where 
(CONVERT(varchar(100), content.CreateTime, 23)  ='{0}' or content.CreateTime is null)  and 
  (staff.GroupLeaderNo='{1}' or AggLeaderNo='{1}' or CommondLeaderNo='{1}' or HrLeaderNo='{1}')", query.Criteria.date, query.Criteria.userNo);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<Health_staff_Model>(dbConn, "staffNo asc", sql, query.PageModel);
                    lr.Data = modelList.ToList<IHealth_staff_Model>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }
            return lr;
        }

        /// <summary>
        /// 保存体检详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<int>> InsertHealthContent(QueryData<InsertHealthContentQuery> query)
        {
            var lr = new DataResult<int>();

            string condition = string.Format(@"insert into health_content(ContentId,titleId,TitleType,Answer,Creator,CreateTime,CreateName) values(@ContentId,@titleId,@TitleType,@Answer,@Creator,@CreateTime,@CreateName)");
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    lr.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, condition, query.Criteria);
                    if (lr.Data < 0)
                    {
                        lr.SetErr("保存体检详情异常", lr.Data);
                    }
                    if (lr.Data == 0)
                    {
                        lr.SetErr("保存体检详情失败", -102);
                    }
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                }
            }
            return lr;
        }
    }
}
