using Lstech.Common.Data;
using Lstech.Common.Helpers;
using Lstech.Entities.Health;
using Lstech.Models.Health;
using Lstech.PC.IHealthService;
using Lstech.PC.IHealthService.Structs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.HealthService
{
    public class HealthAccountService : IHealthAccountService
    {
        public IHealthUserStaff NewHealthUserStaff()
        {
            return new HealthUserStaff();
        }

        public async Task<DataResult<List<IHealthUser>>> GetHealthUserPageAsync(QueryData<HealthUserQuery> query)
        {
            var result = new DataResult<List<IHealthUser>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.AdAccount) ? string.Empty : string.Format(" and AdAccount = '{0}' ", query.Criteria.AdAccount);
            condition += string.IsNullOrEmpty(query.Criteria.UserNo) ? string.Empty : string.Format(" and UserNo like '%{0}%' ", query.Criteria.UserNo);
            condition += string.IsNullOrEmpty(query.Criteria.UserName) ? string.Empty : string.Format(" and UserName like '%{0}%' ", query.Criteria.UserName);
            string sql = string.Format(@"SELECT [Id]
                  ,[UserNo]
                  ,[UserName]
                  ,[AdAccount]
                  ,[Creator]
                  ,[CreateTime]
                  ,[IsAdmin]
              FROM [dbo].[health_user] {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<HealthUser>(dbConn, "Id asc", sql, query.PageModel);
                    result.Data = modelList.ToList<IHealthUser>();
                    result.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        public async Task<DataResult<List<IHealthStaff>>> GetHealthStaffPageAsync(QueryData<HealthStaffQuery> query)
        {
            var result = new DataResult<List<IHealthStaff>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.StaffName) ? string.Empty : string.Format(" and StaffName like '%{0}%' ", query.Criteria.StaffName);
            condition += string.IsNullOrEmpty(query.Criteria.StaffNo) ? string.Empty : string.Format(" and StaffNo = '{0}' ", query.Criteria.StaffNo);
            condition += string.IsNullOrEmpty(query.Criteria.GroupLeader) ? string.Empty : string.Format(" and GroupLeader like '%{0}%' ", query.Criteria.GroupLeader);
            condition += string.IsNullOrEmpty(query.Criteria.GroupLeaderNo) ? string.Empty : string.Format(" and GroupLeaderNo = '{0}' ", query.Criteria.GroupLeaderNo);
            condition += string.IsNullOrEmpty(query.Criteria.AggLeader) ? string.Empty : string.Format(" and AggLeader like '%{0}%' ", query.Criteria.AggLeader);
            condition += string.IsNullOrEmpty(query.Criteria.AggLeaderNo) ? string.Empty : string.Format(" and AggLeaderNo = '{0}' ", query.Criteria.AggLeaderNo);
            condition += string.IsNullOrEmpty(query.Criteria.CommandLeader) ? string.Empty : string.Format(" and CommandLeader like '%{0}%' ", query.Criteria.CommandLeader);
            condition += string.IsNullOrEmpty(query.Criteria.CommondLeaderNo) ? string.Empty : string.Format(" and CommondLeaderNo = '{0}' ", query.Criteria.CommondLeaderNo);
            condition += string.IsNullOrEmpty(query.Criteria.HrLeader) ? string.Empty : string.Format(" and HrLeader like '%{0}%' ", query.Criteria.HrLeader);
            condition += string.IsNullOrEmpty(query.Criteria.HrLeaderNo) ? string.Empty : string.Format(" and HrLeaderNo = '{0}' ", query.Criteria.HrLeaderNo);
            condition += string.IsNullOrEmpty(query.Criteria.GroupType) ? string.Empty : string.Format(" and GroupType = '{0}' ", query.Criteria.GroupType);
            string sql = string.Format(@"SELECT [Id]
                  ,[StaffNo]
                  ,[StaffName]
                  ,[GroupType]
                  ,[GroupLeader]
                  ,[GroupLeaderNo]
                  ,[AggLeader]
                  ,[AggLeaderNo]
                  ,[CommandLeader]
                  ,[CommondLeaderNo]
                  ,[HrLeader]
                  ,[HrLeaderNo]
              FROM [dbo].[health_staff] {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<HealthStaff>(dbConn, "Id desc", sql, query.PageModel);
                    result.Data = modelList.ToList<IHealthStaff>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        public async Task<DataResult<int>> HealthUserStaffSaveAsync(QueryData<HealthUserStaffSaveQuery> query)
        {
            var result = new DataResult<int>();

            string sql = @"insert into dbo.health_user_staff([UserNo],[StaffNo],[Creator],[CreateName],[CreateTime])
                values(@UserNo,@StaffNo,@Creator,@CreateName,getdate())";
            string sqlc = @"select * from dbo.health_user_staff where UserNo=@UserNo and StaffNo=@StaffNo";
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                IDbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    foreach (var item in query.Criteria.LstUserStaff)
                    {
                        result.Data = await MssqlHelper.QueryCountAsync(dbConn, sqlc, new { UserNo = item.UserNo, StaffNo = item.StaffNo }, transaction);
                        if (result.Data > 0)
                        {
                            continue;
                        }
                        result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, item, transaction);
                        if (result.Data <= 0)
                        {
                            transaction.Rollback();
                            result.SetErr("数据保存失败！", -101);
                            return result;
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = -1;
                    transaction.Rollback();
                }
            }

            return result;
        }

        public async Task<DataResult<int>> HealthUserStaffDeleteAsync(QueryData<HealthUserStaffDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sql = @"delete from dbo.health_user_staff where UserNo=@UserNo and StaffNo=@StaffNo";
            string sqlc = @"select * from dbo.health_user_staff where UserNo=@UserNo and StaffNo=@StaffNo";
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    result.Data = await MssqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("数据不存在！", -101);
                        return result;
                    }
                    result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("数据删除失败！", -101);
                    }
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = -1;
                }
            }

            return result;
        }
    }
}
