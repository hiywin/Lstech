using Lstech.Common.Data;
using Lstech.Common.Helpers;
using Lstech.Entities.Frame;
using Lstech.IFrameService;
using Lstech.IFrameService.Structs;
using Lstech.Models.Frame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.FrameService
{
    public class ModuleService : IModuleService
    {
        public async Task<DataResult<List<ISysModuleModel>>> GetModulesAllAsync(QueryData<SysModuleQuery> query)
        {
            var lr = new DataResult<List<ISysModuleModel>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.ModuleNo) ? string.Empty : string.Format(" and ModuleNo = '{0}' ", query.Criteria.ModuleNo);
            condition += string.IsNullOrEmpty(query.Criteria.ModuleName) ? string.Empty : string.Format(" and ModuleName = '{0}' ", query.Criteria.ModuleName);
            condition += query.Criteria.IsDelete == null ? string.Empty : string.Format(" and IsDelete = '{0}' ", query.Criteria.IsDelete);
            string sql = "select Id,ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,IsResource,App,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,Sort " +
                "from sys_module"
                + condition;
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<SysModuleModel>(dbConn, sql, "Sort asc");
                    lr.Data = modelList.ToList<ISysModuleModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }
            //using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(MysqlHelper.MysqlConn))
            //{
            //    try
            //    {
            //        var modelList = await MysqlHelper.QueryListAsync<SysModuleModel>(dbConn, sql, "Sort asc");
            //        lr.Data = modelList.ToList<ISysModuleModel>();
            //    }
            //    catch (Exception ex)
            //    {
            //        lr.SetErr(ex, -101);
            //        lr.Data = null;
            //    }
            //}

            return lr;
        }
    }
}
