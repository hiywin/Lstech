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
        public async Task<DataResult<List<IHealthUser>>> GetHealthUserPageAsync(QueryData<HealthUserQuery> query)
        {
            var result = new DataResult<List<IHealthUser>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.AdAccount) ? string.Empty : string.Format(" and AdAccount = '{0}' ", query.Criteria.AdAccount);
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
    }
}
