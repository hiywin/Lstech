using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.IWCFService.Structs;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthManager.Structs;
using Lstech.PC.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.HealthManager
{
    public class HealthAccountManager : IHealthAccountManager
    {
        public async Task<ErrData<IHealthUser>> HealthUserLoginAsync(QueryData<HealthUserQuery> query)
        {
            var result = new ErrData<IHealthUser>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.GetHealthUserPageAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                if (res.Data.Count <= 0)
                {
                    result.SetInfo("账号无权限，请联系管理员。", -102);
                }
                else
                {
                    var queryEx = new QueryData<WcfADUserGuidQuery>()
                    {
                        Criteria = new WcfADUserGuidQuery()
                        {
                            UserName = query.Criteria.AdAccount,
                            Password = query.Criteria.Pwd
                        }
                    };
                    var resGuid = await WCFOperators.TlgChinaOperater.GetUserADGuidAsync(queryEx);
                    if (string.IsNullOrEmpty(resGuid.Data))
                    {
                        result.SetInfo("用户名或密码错误！", -102);
                    }
                    else
                    {
                        var userModel = res.Data.FirstOrDefault();
                        result.SetInfo(userModel, "登录成功！", 200);
                    }
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ListResult<IHealthUser>> HealthUsersPageAsync(QueryData<HealthUserQuery> query)
        {
            var result = new ListResult<IHealthUser>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.GetHealthUserPageAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    result.Results.Add(item);
                }
                result.SetInfo("成功", 200);
                result.PageModel = res.PageInfo;
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ListResult<IHealthStaff>> HealthStaffsPageAsync(QueryData<HealthStaffQuery> query)
        {
            var result = new ListResult<IHealthStaff>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.GetHealthStaffPageAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    result.Results.Add(item);
                }
                result.SetInfo("成功", 200);
                result.PageModel = res.PageInfo;
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> HealthUserStaffSaveAsync(QueryData<HealthUserStaffModel> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            if(string.IsNullOrEmpty(query.Criteria.UserNo) || query.Criteria.LstStaffNo.Count <= 0)
            {
                result.SetInfo(false, "填写信息不完整！", -102);
            } 
            else
            {
                var lstUserStaff = new List<IHealthUserStaff>();
                foreach (var staffNo in query.Criteria.LstStaffNo)
                {
                    var info = HealthPcOperaters.HealthAccountOperater.NewHealthUserStaff();
                    info.UserNo = query.Criteria.UserNo;
                    info.StaffNo = staffNo;
                    info.Creator = query.Extend.UserNo;
                    info.CreateName = query.Extend.UserName;
                    lstUserStaff.Add(info);
                }
                var queryEx = new QueryData<HealthUserStaffSaveQuery>()
                {
                    Criteria = new HealthUserStaffSaveQuery()
                    {
                        LstUserStaff = lstUserStaff
                    }
                };
                var res = await HealthPcOperaters.HealthAccountOperater.HealthUserStaffSaveAsync(queryEx);
                if (res.HasErr)
                {
                    result.SetInfo(res.ErrMsg, res.ErrCode);
                }
                else
                {
                    result.SetInfo(true, "保存成功。", 200);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> HealthUserStaffDeleteAsync(QueryData<HealthUserStaffDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.HealthUserStaffDeleteAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "删除数据成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
