using Lstech.Common.Data;
using Lstech.Common.Helpers;
using Lstech.Entities.Health;
using Lstech.IWCFService.Structs;
using Lstech.Models.Health;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthManager.Structs;
using Lstech.PC.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        public async Task<ErrData<IHealthUser>> HealthUserLoginExAsync(QueryData<HealthUserQuery> query)
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
                var queryEx = new QueryData<WcfADUserInfoQuery>()
                {
                    Criteria = new WcfADUserInfoQuery()
                    {
                        UserName = query.Criteria.AdAccount,
                        Password = query.Criteria.Pwd
                    }
                };
                var resUser = await WCFOperators.TlgChinaOperater.GetADUserInfoAsync(queryEx);
                if (resUser.HasErr)
                {
                    result.SetInfo(resUser.ErrMsg, resUser.ErrCode);
                }
                else
                {
                    if(string.IsNullOrEmpty(resUser.Data?.UserNo))
                    {
                        result.SetInfo("用户名或密码错误！", -102);
                        result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                        return result;
                    }

                    IHealthUser info = new HealthUser();
                    info.UserNo = resUser.Data.UserNo;
                    info.UserName = resUser.Data.UserName;
                    info.AdAccount = resUser.Data.ADAccount;
                    info.IsAdmin = false;
                    if (res.Data.Count > 0)
                    {
                        info.IsAdmin = res.Data.FirstOrDefault().IsAdmin;
                    }
                    result.SetInfo(info, "登录成功！", 200);
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
                    if (!item.IsAdmin)
                    {
                        result.Results.Add(item);
                    }
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

        public async Task<ErrData<bool>> HealthStaffSaveOrUpdateAsync(QueryData<HealthStaffQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.HealthStaffSaveOrUpdateAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                if (query.Criteria.Id <= 0)
                {
                    result.SetInfo(true, "添加成功！", 200);
                }
                else
                {
                    result.SetInfo(true, "修改成功！", 200);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> HealthStaffDeleteAsync(QueryData<HealthStaffQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            if (query.Criteria.Id <= 0)
            {
                result.SetInfo(false, "请选择需要删除的数据！", -102);
            }
            else
            {
                var res = await HealthPcOperaters.HealthAccountOperater.HealthStaffDeleteAsync(query);
                if (res.HasErr)
                {
                    result.SetInfo(false, res.ErrMsg, res.ErrCode);
                }
                else
                {
                    result.SetInfo(true, "删除成功！", 200);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ListResult<IHealthUserStaffInfo>> HealthUserStaffInfoPageAsync(QueryData<HealthUserStaffInfoQuery> query)
        {
            var result = new ListResult<IHealthUserStaffInfo>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.GetHealthUserStaffInfoPageAsync(query);
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

        public async Task<ListResult<IHealthPowerStaff>> HealthPowerStaffPageAsync(QueryData<HealthPowerStaffQuery> query)
        {
            var result = new ListResult<IHealthPowerStaff>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.GetHealthPowerStaffPageAsync(query);
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

        public async Task<ErrData<bool>> HealthStaffImportAsync(QueryData<Stream> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            if (query.Criteria == null)
            {
                result.SetInfo("请先选择防疫信息表！", -102);
                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                return result;
            }
            //将Excel转换成DataTable
            var tables = EPPlusHelper.ExcelImport(query.Criteria, 2);
            if (tables == null)
            {
                result.SetInfo("防疫信息表有误，请检查！", -102);
            }
            else
            {
                #region 组装员工资料、组长权限、集合组长权限、HR负责人权限

                var lstStaff = new List<IHealthStaff>();
                var lstGroupStaff = new List<IHealthUserStaff>();
                var lstAggStaff = new List<IHealthUserStaff>();
                var lstHrStaff = new List<IHealthUserStaff>();
                foreach (var table in tables)
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            //员工资料
                            var staff = HealthPcOperaters.HealthAccountOperater.NewHealthStaff();//存入health_staff
                            staff.StaffNo = row[2].ToString();
                            staff.StaffName = row[3].ToString();
                            staff.GroupType = row[4].ToString();
                            staff.GroupLeader = row[5].ToString();
                            staff.GroupLeaderNo = row[6].ToString();
                            staff.AggLeader = row[7].ToString();
                            staff.AggLeaderNo = row[8].ToString();
                            staff.CommandLeader = row[9].ToString();
                            staff.CommondLeaderNo = row[10].ToString();
                            staff.HrLeader = row[11].ToString();
                            staff.HrLeaderNo = row[12].ToString();
                            lstStaff.Add(staff);

                            //组长权限
                            var groupStaff = HealthPcOperaters.HealthAccountOperater.NewHealthUserStaff();//存入health_user_staff
                            groupStaff.UserNo= row[6].ToString();
                            groupStaff.StaffNo= row[2].ToString();
                            groupStaff.Creator = query.Extend.UserNo;
                            groupStaff.CreateName = query.Extend.UserName;
                            lstGroupStaff.Add(groupStaff);

                            //集合组长权限
                            var aggStaff = HealthPcOperaters.HealthAccountOperater.NewHealthUserStaff();//存入health_user_staff
                            aggStaff.UserNo = row[8].ToString();
                            aggStaff.StaffNo = row[2].ToString();
                            aggStaff.Creator = query.Extend.UserNo;
                            aggStaff.CreateName = query.Extend.UserName;
                            lstAggStaff.Add(aggStaff);

                            //HR负责人权限
                            var hrStaff = HealthPcOperaters.HealthAccountOperater.NewHealthUserStaff();//存入health_user_staff
                            hrStaff.UserNo = row[12].ToString();
                            hrStaff.StaffNo = row[2].ToString();
                            hrStaff.Creator = query.Extend.UserNo;
                            hrStaff.CreateName = query.Extend.UserName;
                            lstHrStaff.Add(hrStaff);
                        }
                    }
                }

                #endregion
                #region 数据库操作

                if (lstStaff.Count > 0 && lstGroupStaff.Count>0 && lstAggStaff.Count>0 && lstHrStaff.Count>0)
                {
                    var queryEx = new QueryData<HealthStaffImportQuery>()
                    {
                        Criteria = new HealthStaffImportQuery()
                        {
                            LstStaff = lstStaff,
                            LstGroupStaff = lstGroupStaff,
                            LstAggStaff = lstAggStaff,
                            LstHrStaff = lstHrStaff
                        }
                    };
                    var res = await HealthPcOperaters.HealthAccountOperater.HealthStaffImportAsync(queryEx);
                    if (res.HasErr)
                    {
                        result.SetInfo(false, res.ErrMsg, res.ErrCode);
                    }
                    else
                    {
                        result.SetInfo(true, "导入成功！", 200);
                    }
                }
                else
                {
                    result.SetInfo("防疫信息表数据不完整，请检查！", -102);
                }

                #endregion
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
