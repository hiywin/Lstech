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
    /// 组织结构人员信息操作接口实现
    /// </summary>
    public class Health_staffService : IHealth_staffService
    {
        /// <summary>
        /// 根据工号获取组织人员信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealthStaff>>> GetHealthStaffInfo(QueryData<GetHealthStaffInfoQuery> query)
        {
            var lr = new DataResult<List<IHealthStaff>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.StaffNo) ? string.Empty : string.Format(" and StaffNo = '{0}' ", query.Criteria.StaffNo);
            condition += string.IsNullOrEmpty(query.Criteria.StaffName) ? string.Empty : string.Format(" and StaffName = '{0}' ", query.Criteria.StaffName);
            string sql = string.Format(@"SELECT [Id],[StaffNo],[StaffName],[GroupType],[GroupLeader],[GroupLeaderNo],[AggLeader],[AggLeaderNo],[CommandLeader],[CommondLeaderNo],[HrLeader],[HrLeaderNo] FROM health_staff " + condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<HealthStaff>(dbConn, sql, "StaffNo asc");
                    lr.Data = modelList.ToList<IHealthStaff>();
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
