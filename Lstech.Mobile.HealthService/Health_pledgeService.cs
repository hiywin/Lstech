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
    /// 承诺书确认接口实现
    /// </summary>
    public class Health_pledgeService : IHealth_pledgeService
    {
        /// <summary>
        /// 根据工号获取承诺书信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_pledge_Model>>> GetHealthPledgeInfo(QueryData<GetHealthPledgeInfoQuery> query)
        {
            var lr = new DataResult<List<IHealth_pledge_Model>>();

            string condition = @" where 1=1 ";
            condition +=string.IsNullOrEmpty(query.Criteria.StaffNo) ? string.Empty : string.Format(" and StaffNo = '{0}' ", query.Criteria.StaffNo);
            string sql = "SELECT [Id],[StaffNo],[StaffName],[IsSign],[SignTime],[PledgeType] FROM health_pledge " + condition;
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<Health_pledge_Model>(dbConn, sql, "Id asc");
                    lr.Data = modelList.ToList<IHealth_pledge_Model>();
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
        /// 保存确认承诺书信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<int>> InsertHealthPledgeInfo(QueryData<InsertHealthPledgeInfoQuery> query)
        {
            var lr = new DataResult<int>();

            string condition = string.Format(@"insert into health_pledge(StaffNo,StaffName,IsSign,SignTime,PledgeType) values(@StaffNo,@StaffName,@IsSign,@SignTime,@PledgeType)");
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    lr.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, condition, query.Criteria);
                    if (lr.Data < 0)
                    {
                        lr.SetErr("保存确认承诺书异常", lr.Data);
                    }
                    if (lr.Data == 0)
                    {
                        lr.SetErr("保存确认承诺书失败", -102);
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
