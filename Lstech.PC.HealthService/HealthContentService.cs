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
    public class HealthContentService : IHealthContentService
    {
        public async Task<DataResult<List<IHealthContent>>> GetHealthContentPageAsync(QueryData<HealthContentQuery> query)
        {
            var result = new DataResult<List<IHealthContent>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.Answer) ? string.Empty : string.Format(" and Content like '%{0}%' ", query.Criteria.Answer);
            condition += string.IsNullOrEmpty(query.Criteria.Creator) ? string.Empty : string.Format(" and Creator like '%{0}%' ", query.Criteria.Creator);
            string sql = string.Format(@"SELECT [Id]
                  ,[ContentId]
                  ,[TitleId]
                  ,[TitleType]
                  ,[Answer]
                  ,[Creator]
                  ,[CreateTime]
                  ,[CreateName]
              FROM [dbo].[health_content] {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<HealthContent>(dbConn, "Id asc", sql, query.PageModel);
                    result.Data = modelList.ToList<IHealthContent>();
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

        public async Task<DataResult<List<IHealthContentStaff>>> GetHealthContentStaffPageAsync(QueryData<HealthContentQuery> query)
        {
            var result = new DataResult<List<IHealthContentStaff>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.Answer) ? string.Empty : string.Format(" and Content like '%{0}%' ", query.Criteria.Answer);
            condition += string.IsNullOrEmpty(query.Criteria.Creator) ? string.Empty : string.Format(" and Creator like '%{0}%' ", query.Criteria.Creator);
            string sql = string.Format(@"SELECT 
                a.[Id],[ContentId],[TitleId],[TitleType],[Answer],[Creator],[CreateTime],[CreateName]
                ,[StaffNo],[StaffName],[GroupType],[GroupLeader],[GroupLeaderNo],[AggLeader],[AggLeaderNo],[CommandLeader],[CommondLeaderNo],[HrLeader],[HrLeaderNo]
                FROM [dbo].[health_content] a
                LEFT JOIN [dbo].[health_staff] b
                ON a.Creator=b.StaffNo {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<HealthContentStaff>(dbConn, "Id asc", sql, query.PageModel);
                    result.Data = modelList.ToList<IHealthContentStaff>();
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

        public async Task<DataResult<List<IHealthContentStaff>>> GetHealthContentStaffAllAsync(QueryData<HealthContentQuery> query)
        {
            var result = new DataResult<List<IHealthContentStaff>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.Answer) ? string.Empty : string.Format(" and Content like '%{0}%' ", query.Criteria.Answer);
            condition += string.IsNullOrEmpty(query.Criteria.Creator) ? string.Empty : string.Format(" and Creator like '%{0}%' ", query.Criteria.Creator);
            condition += string.IsNullOrEmpty(query.Criteria.CommondLeaderNo) ? string.Empty : string.Format(" and CommondLeaderNo = '{0}' ", query.Criteria.CommondLeaderNo);
            condition += query.Criteria.StarTime == null ? string.Empty : string.Format(" and CreateTime >= '{0}' ", query.Criteria.StarTime);
            condition += query.Criteria.EndTime == null ? string.Empty : string.Format(" and CreateTime <= '{0}' ", query.Criteria.EndTime);
            string sql = string.Format(@"SELECT 
                a.[Id],[ContentId],[TitleId],[TitleType],[Answer],[Creator],[CreateTime],[CreateName]
                ,[StaffNo],[StaffName],[GroupType],[GroupLeader],[GroupLeaderNo],[AggLeader],[AggLeaderNo],[CommandLeader],[CommondLeaderNo],[HrLeader],[HrLeaderNo]
                FROM [dbo].[health_content] a
                LEFT JOIN [dbo].[health_staff] b
                ON a.Creator=b.StaffNo {0} order by Id desc", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<HealthContentStaff>(dbConn, sql);
                    result.Data = modelList.ToList<IHealthContentStaff>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        public async Task<DataResult<List<IHealthContentMain>>> GetHealthContentMainPageAsync(QueryData<HealthContentMainQuery> query)
        {
            var result = new DataResult<List<IHealthContentMain>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.Creator) ? string.Empty : string.Format(" and Creator = '{0}' ", query.Criteria.Creator);
            string sql = string.Format(@"SELECT [Id]
                  ,[ContentId]
                  ,[Creator]
                  ,[CreateName]
                  ,[CreateTime]
              FROM [dbo].[health_content] {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<HealthContentMain>(dbConn, "Id asc", sql, query.PageModel);
                    result.Data = modelList.ToList<IHealthContentMain>();
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

        public async Task<DataResult<List<IHealthContentSub>>> GetHealthContentSubAllAsync(QueryData<HealthContentSubQuery> query)
        {
            var result = new DataResult<List<IHealthContentSub>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.ContentId) ? string.Empty : string.Format(" and ContentId = '{0}' ", query.Criteria.ContentId);
            string sql = string.Format(@"SELECT [Id]
                  ,[ContentId]
                  ,[TitleId]
                  ,[Answer]
              FROM [dbo].[health_content_sub] {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<HealthContentSub>(dbConn, sql);
                    result.Data = modelList.ToList<IHealthContentSub>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        public async Task<DataResult<List<IHealthStaff>>> GetHealthStaffCommandAllAsync(QueryData<HealthStaffQuery> query)
        {
            var result = new DataResult<List<IHealthStaff>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.CommandLeader) ? string.Empty : string.Format(" and CommandLeader like '%{0}%' ", query.Criteria.CommandLeader);
            condition += string.IsNullOrEmpty(query.Criteria.CommondLeaderNo) ? string.Empty : string.Format(" and CommondLeaderNo = '{0}' ", query.Criteria.CommondLeaderNo);
            string sql = string.Format(@"select distinct(CommondLeaderNo),[CommandLeader] from [dbo].[health_staff] {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<HealthStaff>(dbConn, sql);
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
    }
}
