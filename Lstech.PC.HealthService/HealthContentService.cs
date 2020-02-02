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
    }
}
