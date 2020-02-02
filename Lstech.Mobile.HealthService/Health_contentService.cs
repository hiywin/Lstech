using Lstech.Common.Data;
using Lstech.Common.Helpers;
using Lstech.Mobile.IHealthService;
using Lstech.Mobile.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// 保存体检详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<int>> InsertHealthContent(QueryData<InsertHealthContentQuery> query)
        {
            var lr = new DataResult<int>();

            string condition = string.Format(@"insert into health_content(ContentId,TitleId,TitleType,Answer,Creator,CreateTime,CreateName) values(@ContentId,@TitleId,@TitleType,@Answer,@Creator,@CreateTime,@CreateName)");
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
