using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthManager
{
    /// <summary>
    /// 组织人员操作
    /// </summary>
    public class Health_staffServiceManager : IHealth_staffServiceManager
    {
        /// <summary>
        /// 根据工号获取人员信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<HealthStaff>> GetHealthStaffInfoAsync(QueryData<GetHealthStaffInfoQuery> query)
        {
            var result = new ErrData<HealthStaff>();
            HealthStaff healthStaff = null;
            var dt = DateTime.Now;

            var res = await HealthMobileOperaters.Health_staffServiceOperater.GetHealthStaffInfo(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                if (res.Data.Count > 0)
                {
                    healthStaff = new HealthStaff();
                    healthStaff.AggLeader = res.Data[0].AggLeader;
                    healthStaff.AggLeaderNo = res.Data[0].AggLeaderNo;
                    healthStaff.CommandLeader = res.Data[0].CommandLeader;
                    healthStaff.CommondLeaderNo = res.Data[0].CommondLeaderNo;
                    healthStaff.GroupLeader = res.Data[0].GroupLeader;
                    healthStaff.GroupLeaderNo = res.Data[0].GroupLeaderNo;
                    healthStaff.GroupType = res.Data[0].GroupType;
                    healthStaff.HrLeader = res.Data[0].HrLeader;
                    healthStaff.HrLeaderNo = res.Data[0].HrLeaderNo;
                    healthStaff.Id = res.Data[0].Id;
                    healthStaff.StaffName = res.Data[0].StaffName;
                    healthStaff.StaffNo = res.Data[0].StaffNo;
                }

                result.Data = healthStaff;
                result.SetInfo(healthStaff, "获取成功", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
