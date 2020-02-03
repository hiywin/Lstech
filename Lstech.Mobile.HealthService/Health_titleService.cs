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
    /// 体检内容表操作接口实现
    /// </summary>
    public class Health_titleService : IHealth_titleService
    {
        /// <summary>
        /// 获取所有要显示的体检内容表头
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_title_Model>>> GetAllHealthTitle(QueryData<GetAllHealthTitleQuery> query)
        {
            var lr = new DataResult<List<IHealth_title_Model>>();

            string condition = @" where 1=1 and ParentId is null or ParentId = '' "; 
            condition += query.Criteria.IsShow == null ? string.Empty : string.Format(" and IsShow = '{0}' ", query.Criteria.IsShow);
            string sql = "SELECT [Id],[TitleId],[Content],[Type],[IsMustFill],[ParentId],[Creator] ,[CreateTime],[Updator],[UpdateTime],[Sort],[IsShow] " +
                "from health_title"
                + condition;
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<Health_title_Model>(dbConn, sql, "Sort asc");
                    lr.Data = modelList.ToList<IHealth_title_Model>();
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
        /// 获取体检内容表头子选项
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_title_Model>>> GetSubHealthTitle(QueryData<GetSubHealthTitleQuery> query)
        {
            var lr = new DataResult<List<IHealth_title_Model>>();

            string condition = @" where 1=1 and  TitleId != ParentId";
            condition += query.Criteria.IsShow == null ? string.Empty : string.Format(" and IsShow = '{0}' ", query.Criteria.IsShow);
            condition += string.IsNullOrEmpty(query.Criteria.ParentId) ? string.Empty : string.Format(" and ParentId ='{0}' ", query.Criteria.ParentId);
            string sql = "SELECT [Id],[TitleId],[Content],[Type],[IsMustFill],[ParentId],[Creator] ,[CreateTime],[Updator],[UpdateTime],[Sort],[IsShow] " +
                "from health_title"
                + condition;
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<Health_title_Model>(dbConn, sql, "Sort asc");
                    lr.Data = modelList.ToList<IHealth_title_Model>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }
            return lr;
        }
    }
}
