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
using System.Threading.Tasks;

namespace Lstech.PC.HealthService
{
    public class HealthTitleService : IHealthTitleService
    {
        public async Task<DataResult<List<IHealthTitle>>> GetHealthTitleAllAsync(QueryData<HealthTitleQuery> query)
        {
            var result = new DataResult<List<IHealthTitle>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.Content) ? string.Empty : string.Format(" and Content like '%{0}%' ", query.Criteria.Content);
            condition += string.IsNullOrEmpty(query.Criteria.Creator) ? string.Empty : string.Format(" and Creator like '%{0}%' ", query.Criteria.Creator);
            condition += query.Criteria.IsShow == null ? string.Empty : string.Format(" and IsShow = '{0}' ", query.Criteria.IsShow);
            string sql = string.Format(@"SELECT [Id]
                      ,[TitleId]
                      ,[Content]
                      ,[Type]
                      ,[IsMustFill]
                      ,[ParentId]
                      ,[Creator]
                      ,[CreateTime]
                      ,[Updator]
                      ,[UpdateTime]
                      ,[Sort]
                      ,[IsShow]
                  FROM [dbo].[health_title] {0} order by Sort asc", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<HealthTitle>(dbConn, sql);
                    result.Data = modelList.ToList<IHealthTitle>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        public async Task<DataResult<List<IHealthTitle>>> GetHealthTitlePageAsync(QueryData<HealthTitleQuery> query)
        {
            var result = new DataResult<List<IHealthTitle>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.Content) ? string.Empty : string.Format(" and Content like '%{0}%' ", query.Criteria.Content);
            condition += string.IsNullOrEmpty(query.Criteria.Creator) ? string.Empty : string.Format(" and Creator like '%{0}%' ", query.Criteria.Creator);
            string sql = string.Format(@"SELECT [Id]
                      ,[TitleId]
                      ,[Content]
                      ,[Type]
                      ,[IsMustFill]
                      ,[ParentId]
                      ,[Creator]
                      ,[CreateTime]
                      ,[Updator]
                      ,[UpdateTime]
                      ,[Sort]
                      ,[IsShow]
                  FROM [dbo].[health_title] {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<HealthTitle>(dbConn, "Id asc", sql, query.PageModel);
                    result.Data = modelList.ToList<IHealthTitle>();
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

        public async Task<DataResult<int>> HealthTitleSaveAsync(QueryData<HealthTitleSaveQuery> query)
        {
            var result = new DataResult<int>();

            string sql = @"insert into dbo.health_title([TitleId],[Content],[Type],[IsMustFill],[ParentId],[Creator],[CreateTime],[Sort],[IsShow])
                values(@TitleId,@Content,@Type,@IsMustFill,@ParentId,@Creator,getdate(),@Sort,@IsShow)";
            string sqlc = @"select * from dbo.health_title where Content=@Content";
            using (IDbConnection dbConn=MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    result.Data = await MssqlHelper.QueryCountAsync(dbConn, sqlc, new { Content = query.Criteria.Content });
                    if (result.Data > 0)
                    {
                        result.SetErr("标题已存在！", -101);
                        return result;
                    }
                    result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("数据保存失败！", -101);
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

        public async Task<DataResult<int>> HealthTitleUpdateAsync(QueryData<HealthTitleUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sql = @"update dbo.health_title set [Content]=@Content,[Type]=@Type,[IsMustFill]=@IsMustFill,[Updator]=@Updator,[UpdateTime]=getdate(),[Sort]=@Sort,[IsShow]=@IsShow
                where [TitleId]=@TitleId";
            string sqlc = @"select * from dbo.health_title where Content=@Content and TitleId!=@TitleId";
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    result.Data = await MssqlHelper.QueryCountAsync(dbConn, sqlc, new { Content = query.Criteria.Content, TitleId = query.Criteria.TitleId });
                    if (result.Data > 0)
                    {
                        result.SetErr("标题已存在！", -101);
                        return result;
                    }
                    result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("数据更新失败！", -101);
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

        public async Task<DataResult<int>> HealthTitleDeleteAsync(QueryData<HealthTitleDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sql = @"delete from dbo.health_title where [TitleId]=@TitleId";
            string sqlc = @"select * from dbo.health_title where TitleId=@TitleId";
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    result.Data = await MssqlHelper.QueryCountAsync(dbConn, sqlc, new { TitleId = query.Criteria.TitleId });
                    if (result.Data <= 0)
                    {
                        result.SetErr("标题不存在！", -101);
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
